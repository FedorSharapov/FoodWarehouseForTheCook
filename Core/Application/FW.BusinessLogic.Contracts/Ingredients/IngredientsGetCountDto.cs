using FW.EventBus.Interfaces;

namespace FW.BusinessLogic.Contracts.Ingredients
{
    public class IngredientsGetCountDto : IIntegrationEvent
    {
        public Guid UserId { get; set; }
    }
}
