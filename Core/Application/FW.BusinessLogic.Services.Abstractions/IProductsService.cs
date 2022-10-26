using FW.BusinessLogic.Contracts.Products;

namespace FW.BusinessLogic.Services.Abstractions
{
    public interface IProductsService
    {
        /// <summary>
        /// Получить список всех продуктов
        /// </summary>
        /// <param name="userId">идентификатор пользователя</param>
        /// <returns> список всех продуктов </returns>
        public Task<List<ProductResponceDto>> GetAll(Guid userId);
        /// <summary>
        /// Получить список продуктов
        /// </summary>
        /// <param name="pageNumber">номер страницы</param>
        /// <param name="pageSize">объем страницы</param>
        /// <param name="userId">идентификатор пользователя</param>
        /// <returns> список продуктов </returns>
        public Task<List<ProductResponceDto>> GetPaged(int pageNumber, int pageSize, Guid userId);

        /// <summary>
        /// Получить продукт по Id
        /// </summary>
        /// <param name="id"> идентификатор </param>
        /// <param name="userId">идентификатор пользователя</param>
        /// <returns> продукт </returns>
        public Task<ProductResponceDto> GetById(Guid id, Guid userId);

        /// <summary>
        /// Получить количество всех продуктов
        /// </summary>
        /// <param name="userId">идентификатор пользователя</param>
        /// <returns> количество всех продуктов </returns>
        public Task<int> Count(Guid userId);

        /// <summary>
        /// Создать продукт
        /// </summary>
        /// <param name="dto">DTO создания продукта</param>
        /// <returns> идентификатор нового продукта </returns>
        public Task<Guid> Create(ProductCreateDto dto);

        /// <summary>
        /// Изменить продукт
        /// </summary>
        /// <param name="dto">DTO обновления продукта</param>
        /// <returns>true: обновлен, false: не найден </returns>
        public Task<bool> Update(ProductUpdateDto dto);

        /// <summary>
        /// Удалить продукт
        /// </summary>
        /// <param name="id">идентификатор</param>
        /// <param name="userId">идентификатор пользователя</param>
        /// <returns>true: удален, false: не найден </returns>
        public Task<bool> Delete(Guid id, Guid userId);
    }
}
