using FW.BusinessLogic.Contracts.ChangesProducts;

namespace FW.BusinessLogic.Services.Abstractions
{
    public interface IChangesProductsService
    {
        /// <summary>
        /// Получить список всех изменений продуктов
        /// </summary>
        /// <param name="userId">идентификатор пользователя</param>
        /// <returns> список всех изменений продуктов </returns>
        public Task<List<ChangesProductResponseDto>> GetAll(Guid userId);
        /// <summary>
        /// Получить список изменений продуктов
        /// </summary>
        /// <param name="pageNumber">номер страницы</param>
        /// <param name="pageSize">объем страницы</param>
        /// <param name="userId">идентификатор пользователя</param>
        /// <returns> список изменений продуктов </returns>
        public Task<List<ChangesProductResponseDto>> GetPaged(int pageNumber, int pageSize, Guid userId);

        /// <summary>
        /// Получить изменение продукта по Id
        /// </summary>
        /// <param name="id"> идентификатор </param>
        /// <param name="userId">идентификатор пользователя</param>
        /// <returns> изменение продукта </returns>
        public Task<ChangesProductResponseDto> GetById(Guid id, Guid userId);

        /// <summary>
        /// Получить количество всех изменений продуктов
        /// </summary>
        /// <param name="userId">идентификатор пользователя</param>
        /// <returns> количество всех изменений продуктов </returns>
        public Task<int> Count(Guid userId);
    }
}
