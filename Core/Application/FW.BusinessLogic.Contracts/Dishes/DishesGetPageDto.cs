using FW.EventBus.Interfaces;

namespace FW.BusinessLogic.Contracts.Dishes
{
    public class DishesGetPageDto : IIntegrationEvent
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public Guid UserId { get; set; }
    }
}
