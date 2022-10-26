using AutoMapper;
using FW.BusinessLogic.Contracts;
using FW.BusinessLogic.Contracts.Dishes;
using FW.BusinessLogic.Services.Abstractions;
using FW.Domain;
using FW.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace FW.BusinessLogic.Services
{
    public class DishesService : IDishesService
    {
        private readonly ApplicationContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICookingDishService _cookingDishService;

        public DishesService(ApplicationContext dbContext, IMapper mapper, ICookingDishService cookingDishService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _cookingDishService = cookingDishService;
        }

        public async Task<List<DishResponseDto>> GetAll(Guid userId)
        {
            var dishes = await _dbContext.Dishes
                .Where(p => p.UserId == userId)
                .ToListAsync();
            return _mapper.Map<List<DishResponseDto>>(dishes);
        }

        public async Task<List<DishResponseDto>> GetPaged(int pageNumber, int pageSize, Guid userId)
        {
            var dishes = await _dbContext.Dishes
                .Where(p => p.UserId == userId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return _mapper.Map<List<DishResponseDto>>(dishes);
        }

        public async Task<DishResponseDto> GetById(Guid id, Guid userId)
        {
            var dish = await FirstOrDefaultAsync(id, userId);
            return _mapper.Map<DishResponseDto>(dish);
        }

        public async Task<int> Count(Guid userId)
        {
            var count = await _dbContext.Dishes
                .Where(p => p.UserId == userId)
                .CountAsync();
            return count;
        }

        public async Task<Guid> Create(DishCreateDto dto)
        {
            var dish = _mapper.Map<Dishes>(dto);
            dish.ModifiedOn = DateTime.UtcNow;

            await _dbContext.Dishes.AddAsync(dish);
            await _dbContext.SaveChangesAsync();

            return dish.Id;
        }

        public async Task<bool> Update(DishUpdateDto dto)
        {
            var dish = _mapper.Map<Dishes>(dto);

            var entity = await FirstOrDefaultAsync(dish.Id, dish.UserId);
            if (entity != null)
            {
                dish.ModifiedOn = DateTime.UtcNow;
                _dbContext.Entry(entity).CurrentValues.SetValues(dish);
                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> Delete(Guid id, Guid userId)
        {
            var dish = await FirstOrDefaultAsync(id, userId);

            if (dish != null)
            {
                _dbContext.Dishes.Remove(dish);
                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<ResponseStatusResult> Cook(Guid dishId, Guid userId, int numPortions)
        {
            return await _cookingDishService.Cook(dishId, userId, numPortions);
        }

        private async Task<Dishes?> FirstOrDefaultAsync(Guid id, Guid userId)
        {
            return await _dbContext.Dishes
                .Where(p => p.Id == id && p.UserId == userId)
                .FirstOrDefaultAsync();
        }
    }
}
