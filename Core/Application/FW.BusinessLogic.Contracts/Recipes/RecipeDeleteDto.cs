using FW.EventBus.Interfaces;

namespace FW.BusinessLogic.Contracts.Recipes
{
    public class RecipeDeleteDto : IIntegrationEvent
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
    }
}
