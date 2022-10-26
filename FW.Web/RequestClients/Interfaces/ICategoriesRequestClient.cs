using FW.BusinessLogic.Contracts;
using FW.Web.ViewModels.Categories;

namespace FW.Web.RequestClients.Interfaces
{
    public interface ICategoriesRequestClient
    {
        public Task<CategoryResponseVM> Get(Guid id, Guid userId);
        public Task<List<CategoryResponseVM>> GetPage(UInt16 pageNumber, UInt16 pageSize, Guid userId);
        public Task<List<CategoryResponseVM>> GetAll(Guid userId);
        public Task<int> Count(Guid userId);
        public Task<ResponseStatusResult> Create(CategoryVM category, Guid userId);
        public Task<ResponseStatusResult> Update(Guid id, CategoryVM category, Guid userId);
        public Task<ResponseStatusResult> Delete(Guid id, Guid userId);
    }
}
