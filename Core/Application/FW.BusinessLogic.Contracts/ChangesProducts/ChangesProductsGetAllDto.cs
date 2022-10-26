using FW.EventBus.Interfaces;

namespace FW.BusinessLogic.Contracts.ChangesProducts
{
    public class ChangesProductsGetAllDto : IIntegrationEvent
    {
        public Guid UserId { get; set; }
    }
}
