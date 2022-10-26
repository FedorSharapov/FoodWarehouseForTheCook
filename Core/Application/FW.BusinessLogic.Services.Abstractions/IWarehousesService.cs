using FW.BusinessLogic.Contracts.Warehouses;

namespace FW.BusinessLogic.Services.Abstractions
{
    public interface IWarehousesService
    {
        /// <summary>
        /// Получить склад по Id пользователя
        /// </summary>
        /// <param name="userId"> идентификатор пользователя</param>
        /// <returns> склад </returns>
        public Task<WarehouseResponseDto> GetByUserId(Guid userId);

        /// <summary>
        /// Создать склад
        /// </summary>
        /// <param name="dto">DTO создания склада</param>
        /// <returns> true: создан, false: не создан </returns>
        public Task<bool> Create(WarehouseCreateDto dto);
        /// <summary>
        /// Изменить склад
        /// </summary>
        /// <param name="dto">DTO обновления склада</param>
        /// <returns>true: обновлен, false: не найден </returns>
        public Task<bool> Update(WarehouseUpdateDto dto);
    }
}
