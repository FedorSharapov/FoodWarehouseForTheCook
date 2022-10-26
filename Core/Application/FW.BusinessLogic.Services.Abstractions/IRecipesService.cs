using FW.BusinessLogic.Contracts.Recipes;

namespace FW.BusinessLogic.Services.Abstractions
{
    public interface IRecipesService
    {
        /// <summary>
        /// Получить список всех рецептов
        /// </summary>
        /// <param name="userId">идентификатор пользователя</param>
        /// <returns> список всех рецептов </returns>
        public Task<List<RecipeResponseDto>> GetAll(Guid userId);
        /// <summary>
        /// Получить список рецептов
        /// </summary>
        /// <param name="pageNumber">номер страницы</param>
        /// <param name="pageSize">объем страницы</param>
        /// <param name="userId">идентификатор пользователя</param>
        /// <returns> список рецептов </returns>
        public Task<List<RecipeResponseDto>> GetPaged(int pageNumber, int pageSize, Guid userId);

        /// <summary>
        /// Получить рецепт по Id
        /// </summary>
        /// <param name="id"> идентификатор </param>
        /// <param name="userId">идентификатор пользователя</param>
        /// <returns> рецепт </returns>
        public Task<RecipeResponseDto > GetById(Guid id, Guid userId);

        /// <summary>
        /// Получить количество всех рецептов
        /// </summary>
        /// <param name="userId">идентификатор пользователя</param>
        /// <returns> количество всех рецептов </returns>
        public Task<int> Count(Guid userId);

        /// <summary>
        /// Создать рецепт
        /// </summary>
        /// <param name="dto">DTO создания рецепта</param>
        /// <returns> идентификатор нового рецепта </returns>
        public Task<Guid> Create(RecipeCreateDto dto);

        /// <summary>
        /// Изменить рецепт
        /// </summary>
        /// <param name="dto">DTO обновления рецепта</param>
        /// <returns>true: обновлен, false: не найден </returns>
        public Task<bool> Update(RecipeUpdateDto dto);

        /// <summary>
        /// Удалить рецепт
        /// </summary>
        /// <param name="id">идентификатор</param>
        /// <param name="userId">идентификатор пользователя</param>
        /// <returns>true: удален, false: не найден </returns>
        public Task<bool> Delete(Guid id, Guid userId);
    }
}
