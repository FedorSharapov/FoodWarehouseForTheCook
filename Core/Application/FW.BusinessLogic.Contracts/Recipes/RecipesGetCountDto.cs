using FW.EventBus.Interfaces;

namespace FW.BusinessLogic.Contracts.Recipes
{
    public class RecipesGetCountDto : IIntegrationEvent 
    {
        public Guid UserId { get; set; }
    }
}
