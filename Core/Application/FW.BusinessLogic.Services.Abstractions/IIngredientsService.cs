using FW.BusinessLogic.Contracts.Ingredients;

namespace FW.BusinessLogic.Services.Abstractions
{
    public interface IIngredientsService
    {
        /// <summary>
        /// Получить список всех ингредиентов
        /// </summary>
        /// <param name="userId">идентификатор пользователя</param>
        /// <returns> список всех ингредиентов </returns>
        public Task<List<IngredientResponseDto>> GetAll(Guid userId);

        /// <summary>
        /// Получить список ингредиентов
        /// </summary>
        /// <param name="pageNumber">номер страницы</param>
        /// <param name="pageSize">объем страницы</param>
        /// <param name="userId">идентификатор пользователя</param>
        /// <returns> список ингредиентов </returns>
        public Task<List<IngredientResponseDto>> GetPaged(int pageNumber, int pageSize, Guid userId);

        /// <summary>
        /// Получить ингредиент по Id
        /// </summary>
        /// <param name="id"> идентификатор </param>
        /// <param name="userId">идентификатор пользователя</param>
        /// <returns> ингредиент </returns>
        public Task<IngredientResponseDto> GetById(Guid id, Guid userId);

        /// <summary>
        /// Получить количество всех ингредиентов
        /// </summary>
        /// <param name="userId">идентификатор пользователя</param>
        /// <returns> количество всех ингредиентов </returns>
        public Task<int> Count(Guid userId);

        /// <summary>
        /// Создать ингредиент
        /// </summary>
        /// <param name="dto">DTO создания ингредиента</param>
        /// <returns> идентификатор нового ингредиента </returns>
        public Task<Guid> Create(IngredientCreateDto dto);

        /// <summary>
        /// Изменить ингредиент
        /// </summary>
        /// <param name="dto">DTO обновления ингредиента</param>
        /// <returns>true: обновлен, false: не найден </returns>
        public Task<bool> Update(IngredientUpdateDto dto);

        /// <summary>
        /// Удалить ингредиент
        /// </summary>
        /// <param name="id">идентификатор</param>
        /// <param name="userId">идентификатор пользователя</param>
        /// <returns>true: удален, false: не найден </returns>
        public Task<bool> Delete(Guid id, Guid userId);
    }
}
