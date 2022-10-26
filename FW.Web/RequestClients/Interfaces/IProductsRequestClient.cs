using FW.BusinessLogic.Contracts;
using FW.Web.ViewModels.Products;

namespace FW.Web.RequestClients.Interfaces
{
    public interface IProductsRequestClient
    {
        public Task<ProductResponseVM> Get(Guid id, Guid userId);
        public Task<List<ProductResponseVM>> GetPage(UInt16 pageNumber, UInt16 pageSize, Guid userId);
        public Task<List<ProductResponseVM>> GetAll(Guid userId);
        public Task<int> Count(Guid userId);
        public Task<ResponseStatusResult> Create(ProductVM product, Guid userId);
        public Task<ResponseStatusResult> Update(Guid id, ProductVM product, Guid userId);
        public Task<ResponseStatusResult> Delete(Guid id, Guid userId);
    }
}
