using FW.EventBus.Interfaces;

namespace FW.BusinessLogic.Contracts.Dishes
{
    public class DishesGetCountDto : IIntegrationEvent 
    {
        public Guid UserId { get; set; }
    }
}
