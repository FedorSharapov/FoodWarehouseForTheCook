using AutoMapper;
using FW.BusinessLogic.Contracts.Warehouses;
using FW.BusinessLogic.Services.Abstractions;
using FW.Domain;
using FW.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace FW.BusinessLogic.Services
{
    public class WarehousesService : IWarehousesService
    {
        private readonly ApplicationContext _dbContext;
        private readonly IMapper _mapper;

        public WarehousesService(ApplicationContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<WarehouseResponseDto > GetByUserId(Guid userId)
        {
            var warehouse = await FirstOrDefaultAsync(userId);
            return _mapper.Map<WarehouseResponseDto >(warehouse);
        }

        public async Task<bool> Create(WarehouseCreateDto dto)
        {
            var warehouse = _mapper.Map<Warehouses>(dto);

            var entity = await FirstOrDefaultAsync(warehouse.UserId);
            if (entity == null)     // возможно создать только один склад на одного пользователя
            {
                warehouse.ModifiedOn = DateTime.UtcNow;

                await _dbContext.Warehouses.AddAsync(warehouse);
                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> Update(WarehouseUpdateDto dto)
        {
            var warehouse = _mapper.Map<Warehouses>(dto);
            var entity = await FirstOrDefaultAsync(warehouse.UserId);
            if (entity != null)
            {
                warehouse.Id = entity.Id;
                warehouse.ModifiedOn = DateTime.UtcNow;
                
                _dbContext.Entry(entity).CurrentValues.SetValues(warehouse);
                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        private async Task<Warehouses?> FirstOrDefaultAsync(Guid userId)
        {
            return await _dbContext.Warehouses
                .Where(p => p.UserId == userId)
                .FirstOrDefaultAsync();
        }
    }
}
