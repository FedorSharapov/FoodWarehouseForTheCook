using FW.EventBus.Interfaces;

namespace FW.BusinessLogic.Contracts.Recipes
{
    public class RecipesGetPageDto : IIntegrationEvent
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public Guid UserId { get; set; }
    }
}
