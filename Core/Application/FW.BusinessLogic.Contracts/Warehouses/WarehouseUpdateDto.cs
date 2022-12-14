using FW.EventBus.Interfaces;

namespace FW.BusinessLogic.Contracts.Warehouses
{
    public class WarehouseUpdateDto : IIntegrationEvent
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public Guid UserId { get; set; }
    }
}
