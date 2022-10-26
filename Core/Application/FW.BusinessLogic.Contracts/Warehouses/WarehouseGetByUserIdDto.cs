using FW.EventBus.Interfaces;

namespace FW.BusinessLogic.Contracts.Warehouses
{
    public class WarehouseGetByUserIdDto : IIntegrationEvent
    {
        public Guid UserId { get; set; }
    }
}
