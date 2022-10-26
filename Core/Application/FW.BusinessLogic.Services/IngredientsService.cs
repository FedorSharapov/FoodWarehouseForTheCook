using AutoMapper;
using FW.BusinessLogic.Contracts.Ingredients;
using FW.BusinessLogic.Services.Abstractions;
using FW.Domain;
using FW.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace FW.BusinessLogic.Services
{
    public class IngredientsService : IIngredientsService
    {
        private readonly ApplicationContext _dbContext;
        private readonly IMapper _mapper;

        public IngredientsService(ApplicationContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<IngredientResponseDto>> GetAll(Guid userId)
        {
            var ingredients = await _dbContext.Ingredients
                .Where(p => p.UserId == userId)
                .ToListAsync();
            return _mapper.Map<List<IngredientResponseDto>>(ingredients);
        }

        public async Task<List<IngredientResponseDto>> GetPaged(int pageNumber, int pageSize, Guid userId)
        {
            var ingredients = await _dbContext.Ingredients
                .Where(p => p.UserId == userId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return _mapper.Map<List<IngredientResponseDto>>(ingredients);
        }

        public async Task<IngredientResponseDto> GetById(Guid id, Guid userId)
        {
            var ingredient = await FirstOrDefaultAsync(id, userId);
            return _mapper.Map<IngredientResponseDto>(ingredient);
        }

        public async Task<int> Count(Guid userId)
        {
            var count = await _dbContext.Ingredients
                .Where(p => p.UserId == userId)
                .CountAsync();
            return count;
        }

        public async Task<Guid> Create(IngredientCreateDto dto)
        {
            var ingredient = _mapper.Map<Ingredients>(dto);
            ingredient.ModifiedOn = DateTime.UtcNow;

            await _dbContext.Ingredients.AddAsync(ingredient);
            await _dbContext.SaveChangesAsync();

            return ingredient.Id;
        }
        public async Task<bool> Update(IngredientUpdateDto dto)
        {
            var ingredient = _mapper.Map<Ingredients>(dto);

            var entity = await FirstOrDefaultAsync(ingredient.Id, ingredient.UserId);
            if (entity != null)
            {
                ingredient.ModifiedOn = DateTime.UtcNow;
                _dbContext.Entry(entity).CurrentValues.SetValues(ingredient);
                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> Delete(Guid id, Guid userId)
        {
            var ingredient = await FirstOrDefaultAsync(id, userId);

            if (ingredient != null)
            {
                _dbContext.Ingredients.Remove(ingredient);
                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        private async Task<Ingredients?> FirstOrDefaultAsync(Guid id, Guid userId)
        {
            return await _dbContext.Ingredients
                .Where(p => p.Id == id && p.UserId == userId)
                .FirstOrDefaultAsync();
        }
    }
}
