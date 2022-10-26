using FW.EventBus.Interfaces;

namespace FW.BusinessLogic.Contracts.Ingredients
{
    public class IngredientsGetPageDto : IIntegrationEvent
    {
        public UInt16 PageNumber { get; set; }
        public UInt16 PageSize { get; set; }

        public Guid UserId { get; set; }
    }
}
