using AutoMapper;
using FW.BusinessLogic.Contracts.Category;
using FW.BusinessLogic.Services.Abstractions;
using FW.Domain;
using FW.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace FW.BusinessLogic.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ApplicationContext _dbContext;
        private readonly IMapper _mapper;

        public CategoriesService(ApplicationContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<CategoryResponseDto>> GetAll(Guid userId)
        {
            var categories = await _dbContext.Categories
                .Where(p => p.UserId == userId)
                .ToListAsync();
            return _mapper.Map<List<CategoryResponseDto>>(categories);
        }

        public async Task<List<CategoryResponseDto>> GetPaged(int pageNumber, int pageSize, Guid userId)
        {
            var categories = await _dbContext.Categories
                .Where(p => p.UserId == userId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return _mapper.Map<List<CategoryResponseDto>>(categories);
        }

        public async Task<CategoryResponseDto> GetById(Guid id, Guid userId)
        {
            var category = await FirstOrDefaultAsync(id, userId);
            return _mapper.Map<CategoryResponseDto>(category);
        }

        public async Task<int> Count(Guid userId)
        {
            var count = await _dbContext.Categories
                .Where(p => p.UserId == userId)
                .CountAsync();
            return count;
        }

        public async Task<Guid> Create(CategoryCreateDto dto)
        {
            var category = _mapper.Map<Categories>(dto);
            category.ModifiedOn = DateTime.UtcNow;

            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();
            return category.Id;
        }

        public async Task<bool> Update(CategoryUpdateDto dto)
        {
            var category = _mapper.Map<Categories>(dto);

            var entity = await FirstOrDefaultAsync(category.Id, category.UserId);
            if (entity != null)
            {
                category.ModifiedOn = DateTime.UtcNow;
                _dbContext.Entry(entity).CurrentValues.SetValues(category);
                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> Delete(Guid id, Guid userId)
        {
            var category = await FirstOrDefaultAsync(id, userId);
            if (category != null)
            {
                _dbContext.Categories.Remove(category);
                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        private async Task<Categories?> FirstOrDefaultAsync(Guid id, Guid userId)
        {
            return await _dbContext.Categories
                .Where(p => p.Id == id && p.UserId == userId)
                .FirstOrDefaultAsync();
        }
    }
}
