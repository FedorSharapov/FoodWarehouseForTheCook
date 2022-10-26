using FW.BusinessLogic.Contracts;
using FW.BusinessLogic.Contracts.Dishes;

namespace FW.BusinessLogic.Services.Abstractions
{
    public interface IDishesService
    {
        /// <summary>
        /// Получить список всех блюд
        /// </summary>
        /// <param name="userId">идентификатор пользователя</param>
        /// <returns> список всех блюд </returns>
        public Task<List<DishResponseDto>> GetAll(Guid userId);
        /// <summary>
        /// Получить список блюд
        /// </summary>
        /// <param name="pageNumber">номер страницы</param>
        /// <param name="pageSize">объем страницы</param>
        /// <param name="userId">идентификатор пользователя</param>
        /// <returns> список блюд </returns>
        public Task<List<DishResponseDto>> GetPaged(int pageNumber, int pageSize, Guid userId);

        /// <summary>
        /// Получить блюдо по Id
        /// </summary>
        /// <param name="id"> идентификатор </param>
        /// <param name="userId">идентификатор пользователя</param>
        /// <returns> блюдо </returns>
        public Task<DishResponseDto> GetById(Guid id, Guid userId);

        /// <summary>
        /// Получить количество всех блюд
        /// </summary>
        /// <param name="userId">идентификатор пользователя</param>
        /// <returns> количество всех блюд </returns>
        public Task<int> Count(Guid userId);

        /// <summary>
        /// Создать блюдо
        /// </summary>
        /// <param name="dto">DTO создания блюда</param>
        /// <returns> идентификатор нового блюда </returns>
        public Task<Guid> Create(DishCreateDto dto);

        /// <summary>
        /// Изменить блюдо
        /// </summary>
        /// <param name="dto">DTO обновления блюда</param>
        /// <returns>true: обновлен, false: не найден </returns>
        public Task<bool> Update(DishUpdateDto dto);

        /// <summary>
        /// Удалить блюдо
        /// </summary>
        /// <param name="id">идентификатор</param>
        /// <param name="userId">идентификатор пользователя</param>
        /// <returns>true: удален, false: не найден </returns>
        public Task<bool> Delete(Guid id, Guid userId);

        /// <summary>
        /// Приготовить блюдо
        /// </summary>
        /// <param name="dishId">идентификатор блюда</param>
        /// <param name="userId">идентификатор пользователя</param>
        /// <param name="numPortions">количество порций</param>
        /// <returns>true: приготовлено, false: нет </returns>
        public Task<ResponseStatusResult> Cook(Guid dishId, Guid userId, int numPortions);
    }
}
