using FW.EventBus.Interfaces;

namespace FW.BusinessLogic.Contracts.Recipes
{
    public class RecipesGetAllDto : IIntegrationEvent
    {
        public Guid UserId { get; set; }
    }
}
