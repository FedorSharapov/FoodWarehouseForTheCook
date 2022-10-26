using FW.BusinessLogic.Contracts;
using FW.Web.ViewModels.Warehouses;

namespace FW.Web.RpcClients.Interfaces
{
    public interface IWarehousesRpcClient
    {
        public Task<WarehouseResponseVM> GetByUserId(Guid userId);
        public Task<ResponseStatusResultWithoutId> Create(WarehouseVM warehouse, Guid userId);
        public Task<ResponseStatusResultWithoutId> Update(WarehouseVM warehouse, Guid userId);
    }
}
