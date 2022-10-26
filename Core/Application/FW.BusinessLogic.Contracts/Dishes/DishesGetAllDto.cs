using FW.EventBus.Interfaces;

namespace FW.BusinessLogic.Contracts.Dishes
{
    public class DishesGetAllDto : IIntegrationEvent
    {
        public Guid UserId { get; set; }
    }
}
