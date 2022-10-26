using FW.EventBus.Interfaces;

namespace FW.BusinessLogic.Contracts.ChangesProducts
{
    public class ChangesProductsGetPageDto : IIntegrationEvent
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public Guid UserId { get; set; }
    }
}
