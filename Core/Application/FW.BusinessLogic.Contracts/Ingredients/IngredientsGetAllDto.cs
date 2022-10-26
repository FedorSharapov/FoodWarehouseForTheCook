using FW.EventBus.Interfaces;

namespace FW.BusinessLogic.Contracts.Ingredients
{
    public class IngredientsGetAllDto : IIntegrationEvent
    {
        public Guid UserId { get; set; }
    }
}
