using FW.EventBus.Interfaces;

namespace FW.BusinessLogic.Contracts.ChangesProducts
{
    public class ChangesProductsGetCountDto : IIntegrationEvent 
    {
        public Guid UserId { get; set; }
    }
}
