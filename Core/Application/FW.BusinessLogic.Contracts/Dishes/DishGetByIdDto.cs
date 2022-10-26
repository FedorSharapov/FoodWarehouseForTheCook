using FW.EventBus.Interfaces;

namespace FW.BusinessLogic.Contracts.Dishes
{
    public class DishGetByIdDto : IIntegrationEvent
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
