using AutoMapper;
using FW.BusinessLogic.Contracts.Recipes;
using FW.BusinessLogic.Services.Abstractions;
using FW.Domain;
using FW.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace FW.BusinessLogic.Services
{
    public class RecipesService : IRecipesService
    {
        private readonly ApplicationContext _dbContext;
        private readonly IMapper _mapper;

        public RecipesService(ApplicationContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<RecipeResponseDto>> GetAll(Guid userId)
        {
            var recipes = await _dbContext.Recipes
                .Where(p => p.UserId == userId)
                .ToListAsync();
            return _mapper.Map<List<RecipeResponseDto>>(recipes);
        }

        public async Task<List<RecipeResponseDto >> GetPaged(int pageNumber, int pageSize, Guid userId)
        {
            var recipes = await _dbContext.Recipes
                .Where(p => p.UserId == userId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return _mapper.Map<List<RecipeResponseDto >>(recipes);
        }

        public async Task<RecipeResponseDto > GetById(Guid id, Guid userId)
        {
            var recipe = await FirstOrDefaultAsync(id, userId);
            return _mapper.Map<RecipeResponseDto >(recipe);
        }

        public async Task<int> Count(Guid userId)
        {
            var count = await _dbContext.Recipes
                .Where(p => p.UserId == userId)
                .CountAsync();
            return count;
        }

        public async Task<Guid> Create(RecipeCreateDto dto)
        {
            var recipe = _mapper.Map<Recipes>(dto);
            recipe.ModifiedOn = DateTime.UtcNow;

            var dish = await DishesFirstOrDefaultAsync(recipe.DishesId, recipe.UserId);
            var ingredient = await IngredientsFirstOrDefaultAsync(recipe.IngredientId, recipe.UserId);
            if (dish && ingredient)
            {
                await _dbContext.Recipes.AddAsync(recipe);
                await _dbContext.SaveChangesAsync();
                return recipe.Id;
            }
            /*else
                throw new Exception($"Dish {recipe.DishesId} does not exist!");*/

            return Guid.Empty;
        }

        public async Task<bool> Update(RecipeUpdateDto dto)
        {
            var recipe = _mapper.Map<Recipes>(dto);

            var dish = await DishesFirstOrDefaultAsync(recipe.DishesId, recipe.UserId);
            var ingredient = await IngredientsFirstOrDefaultAsync(recipe.IngredientId, recipe.UserId);
            if (!dish || !ingredient)
                return false;

            var entity = await FirstOrDefaultAsync(recipe.Id, recipe.UserId);
            if (entity != null)
            {
                recipe.ModifiedOn = DateTime.UtcNow;
                _dbContext.Entry(entity).CurrentValues.SetValues(recipe);
                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> Delete(Guid id, Guid userId)
        {
            var recipe = await FirstOrDefaultAsync(id, userId);
            if (recipe != null)
            {
                _dbContext.Recipes.Remove(recipe);
                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        private async Task<Recipes?> FirstOrDefaultAsync(Guid id, Guid userId)
        {
            return await _dbContext.Recipes
                .Where(p => p.Id == id && p.UserId == userId)
                .FirstOrDefaultAsync();
        }
        private async Task<bool> IngredientsFirstOrDefaultAsync(Guid ingredientId, Guid userId)
        {
            return (await _dbContext.Ingredients
                .Where(p => p.Id == ingredientId && p.UserId == userId)
                .FirstOrDefaultAsync() != null) ? true : false;
        }
        private async Task<bool> DishesFirstOrDefaultAsync(Guid dishId, Guid userId)
        {
            return (await _dbContext.Dishes
                .Where(p => p.Id == dishId && p.UserId == userId)
                .FirstOrDefaultAsync() != null)? true : false;
        }
    }
}
