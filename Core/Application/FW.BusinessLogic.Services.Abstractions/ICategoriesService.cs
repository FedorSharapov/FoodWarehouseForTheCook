using FW.BusinessLogic.Contracts.Category;

namespace FW.BusinessLogic.Services.Abstractions
{
    public interface ICategoriesService
    {
        /// <summary>
        /// Получить список всех категорий
        /// </summary>
        /// <param name="userId">идентификатор пользователя</param>
        /// <returns> список всех категорий </returns>
        public Task<List<CategoryResponseDto>> GetAll(Guid userId);
        /// <summary>
        /// Получить список категорий
        /// </summary>
        /// <param name="pageNumber">номер страницы</param>
        /// <param name="pageSize">объем страницы</param>
        /// <param name="userId">идентификатор пользователя</param>
        /// <returns> список категорий </returns>
        public Task<List<CategoryResponseDto>> GetPaged(int pageNumber, int pageSize, Guid userId);

        /// <summary>
        /// Получить категорию по Id
        /// </summary>
        /// <param name="id"> идентификатор </param>
        /// <param name="userId">идентификатор пользователя</param>
        /// <returns> категория </returns>
        public Task<CategoryResponseDto> GetById(Guid id, Guid userId);

        /// <summary>
        /// Получить количество всех категорий
        /// </summary>
        /// <param name="userId">идентификатор пользователя</param>
        /// <returns> количество всех категорий </returns>
        public Task<int> Count(Guid userId);

        /// <summary>
        /// Создать категорию
        /// </summary>
        /// <param name="dto">DTO категории</param>
        /// <returns> идентификатор новой категории </returns>
        public Task<Guid> Create(CategoryCreateDto dto);

        /// <summary>
        /// Изменить категорию
        /// </summary>
        /// <param name="dto">DTO обновления категории</param>
        /// <returns>true: обновлена, false: не найдена </returns>

        public Task<bool> Update(CategoryUpdateDto dto);
        /// <summary>
        /// Удалить категорию
        /// </summary>
        /// <param name="id">идентификатор</param>
        /// <param name="userId">идентификатор пользователя</param>
        /// <returns>true: удалена, false: не найдена </returns>
        public Task<bool> Delete(Guid id, Guid userId);
    }
}
