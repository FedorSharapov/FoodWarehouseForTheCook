using AutoMapper;
using FW.BusinessLogic.Contracts.Products;
using FW.BusinessLogic.Services.Abstractions;
using FW.Domain;
using FW.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace FW.BusinessLogic.Services
{
    public class ProductsService : IProductsService
    {
        private readonly ApplicationContext _dbContext;
        private readonly IMapper _mapper;

        public ProductsService(ApplicationContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<ProductResponceDto>> GetAll(Guid userId)
        {
            var products = await _dbContext.Products
                .Where(p => p.UserId == userId)
                .ToListAsync();
            return _mapper.Map<List<ProductResponceDto>>(products);
        }

        public async Task<List<ProductResponceDto>> GetPaged(int pageNumber, int pageSize, Guid userId)
        {
            var products = await _dbContext.Products
                .Where(p => p.UserId == userId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return _mapper.Map<List<ProductResponceDto>>(products);
        }

        public async Task<ProductResponceDto> GetById(Guid id, Guid userId)
        {
            var product = await FirstOrDefaultAsync(id, userId);
            return _mapper.Map<ProductResponceDto>(product);
        }

        public async Task<int> Count(Guid userId)
        {
            var count = await _dbContext.Products
                .Where(p => p.UserId == userId)
                .CountAsync();
            return count;
        }

        public async Task<Guid> Create(ProductCreateDto dto)
        {
            var product = _mapper.Map<Products>(dto);
            product.ModifiedOn = DateTime.UtcNow;

            var warehouse = await WarehouseFirstOrDefaultAsync(product.WarehouseId, product.UserId);
            var category = await CategoryFirstOrDefaultAsync(product.CategoryId, product.UserId);
            var ingredient = await IngredientFirstOrDefaultAsync(product.IngredientId, product.UserId);
            if (warehouse && category && ingredient)
            {
                await _dbContext.Products.AddAsync(product);
                await _dbContext.SaveChangesAsync();

                await ChangesProductCreate(product.UserId, product.Id, product.Quantity);

                return product.Id;
            }
            /*else
                throw new Exception($"Category {product.CategoryId} does not exist!");*/

            return Guid.Empty;
        }

        public async Task<bool> Update(ProductUpdateDto dto)
        {
            var product = _mapper.Map<Products>(dto);

            var warehouse = await WarehouseFirstOrDefaultAsync(product.WarehouseId, product.UserId);
            var category = await CategoryFirstOrDefaultAsync(product.CategoryId, product.UserId);
            var ingredient = await IngredientFirstOrDefaultAsync(product.IngredientId, product.UserId);
            if (!warehouse || !category || !ingredient)
                return false;

            var entity = await FirstOrDefaultAsync(product.Id, product.UserId);
            if (entity != null)
            {
                var productQuantityChanges = product.Quantity - entity.Quantity;

                product.ModifiedOn = DateTime.UtcNow;
                _dbContext.Entry(entity).CurrentValues.SetValues(product);
                await _dbContext.SaveChangesAsync();

                await ChangesProductCreate(product.UserId, product.Id, productQuantityChanges);

                return true;
            }

            return false;
        }

        public async Task<bool> Delete(Guid id, Guid userId)
        {
            var product = await FirstOrDefaultAsync(id, userId);
            if (product != null)
            {
                var productId = product.Id;
                var productQuantityChanges = -product.Quantity;

                _dbContext.Products.Remove(product);
                await _dbContext.SaveChangesAsync();

                await ChangesProductCreate(userId, productId, productQuantityChanges);
                return true;
            }

            return false;
        }

        private async Task ChangesProductCreate(Guid userId, Guid productId, int quantity)
        {
            var changesProduct = new ChangesProducts
            {
                UserId = userId,
                ModifiedOn = DateTime.UtcNow,
                ProductId = productId,
                Quantity = quantity
            };
            await _dbContext.ChangesProducts.AddAsync(changesProduct);
            await _dbContext.SaveChangesAsync();
        }

        private async Task<bool> WarehouseFirstOrDefaultAsync(Guid warehouseId, Guid userId)
        {
            return (await _dbContext.Warehouses
                .Where(p => p.Id == warehouseId && p.UserId == userId)
                .FirstOrDefaultAsync() != null) ? true : false;
        }
        private async Task<bool> IngredientFirstOrDefaultAsync(Guid ingredientId, Guid userId)
        {
            return (await _dbContext.Ingredients
                .Where(p => p.Id == ingredientId && p.UserId == userId)
                .FirstOrDefaultAsync() != null) ? true : false;
        }
        private async Task<bool> CategoryFirstOrDefaultAsync(Guid categoryId, Guid userId)
        {
            return (await _dbContext.Categories
                .Where(p => p.Id == categoryId && p.UserId == userId)
                .FirstOrDefaultAsync() != null) ? true : false;
        }

        private async Task<Products?> FirstOrDefaultAsync(Guid id, Guid userId)
        {
            return await _dbContext.Products
                .Where(p => p.Id == id && p.UserId == userId)
                .FirstOrDefaultAsync();
        }
    }
}
