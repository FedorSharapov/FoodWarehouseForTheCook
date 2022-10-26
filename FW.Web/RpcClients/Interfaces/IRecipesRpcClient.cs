using FW.BusinessLogic.Contracts;
using FW.Web.ViewModels.Recipes;

namespace FW.Web.RpcClients.Interfaces
{
    public interface IRecipesRpcClient
    {
        public Task<RecipeResponseVM> GetById(Guid id, Guid userId);
        public Task<List<RecipeResponseVM>> GetPage(UInt16 pageNumber, UInt16 pageSize, Guid userId);
        public Task<List<RecipeResponseVM>> GetAll(Guid userId);
        public Task<int> Count(Guid userId);
        public Task<ResponseStatusResult> Create(RecipeVM recipe, Guid userId);
        public Task<ResponseStatusResult> Update(Guid id, RecipeVM recipe, Guid userId);
        public Task<ResponseStatusResult> Delete(Guid id, Guid userId);
    }
}
