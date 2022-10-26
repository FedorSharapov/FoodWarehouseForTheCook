using FW.BusinessLogic.Contracts;
using FW.Web.ViewModels.Ingredients;

namespace FW.Web.RpcClients.Interfaces
{
    public interface IIngredientsRpcClient
    {
        public Task<IngredientResponseVM> GetById(Guid id, Guid userId);
        public Task<List<IngredientResponseVM>> GetPage(UInt16 pageNumber, UInt16 pageSize, Guid userId);
        public Task<List<IngredientResponseVM>> GetAll(Guid userId);
        public Task<int> Count(Guid userId);
        public Task<ResponseStatusResult> Create(IngredientVM ingredient, Guid userId);
        public Task<ResponseStatusResult> Update(Guid id, IngredientVM ingredient, Guid userId);
        public Task<ResponseStatusResult> Delete(Guid id, Guid userId);
    }
}
