using FW.BusinessLogic.Contracts;
using FW.Web.ViewModels.Dishes;

namespace FW.Web.RpcClients.Interfaces
{
    public interface IDishesRpcClient
    {
        public Task<DishResponseVM> GetById(Guid id, Guid userId);
        public Task<List<DishResponseVM>> GetPage(UInt16 pageNumber, UInt16 pageSize, Guid userId);
        public Task<List<DishResponseVM>> GetAll(Guid userId);
        public Task<int> Count(Guid userId);
        public Task<ResponseStatusResult> Create(DishVM dish, Guid userId);
        public Task<ResponseStatusResult> Update(Guid id, DishVM dish, Guid userId);
        public Task<ResponseStatusResult> Delete(Guid id, Guid userId);
        public Task<ResponseStatusResult> Cook(Guid id, Guid userId, int numPortions);
    }
}
