using FW.BusinessLogic.Contracts;
using FW.Web.ViewModels.ChangesProducts;

namespace FW.Web.RpcClients.Interfaces
{
    public interface IChangesProductsRpcClient
    {
        public Task<ChangesProductResponseVM> GetById(Guid id, Guid userId);
        public Task<List<ChangesProductResponseVM>> GetPage(UInt16 pageNumber, UInt16 pageSize, Guid userId);
        public Task<List<ChangesProductResponseVM>> GetAll(Guid userId);
        public Task<int> Count(Guid userId);
    }
}
