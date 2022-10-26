using FW.BusinessLogic.Contracts.ChangesProducts;
using FW.BusinessLogic.Services.Abstractions;
using FW.EventBus;
using FW.EventBus.Interfaces;

namespace FW.Management.EventHandlers.ChangesProducts
{
    public class ChangesProductsGetPageEventHandler : IIntegrationEventHandler<ChangesProductsGetPageDto>
    {
        private readonly ILogger _logger;
        private readonly IChangesProductsService _changesProductsService;

        public ChangesProductsGetPageEventHandler(ILogger logger, IChangesProductsService changesProductsService)
        {
            _logger = logger;
            _changesProductsService = changesProductsService;
        }

        public async Task Handle(IntegrationContext<ChangesProductsGetPageDto> msgContext)
        {
            _logger.Information($"Received a message from exchange/queue: {msgContext.ExchangeName}/{msgContext.QueueName}");

            int page = msgContext.Message.PageNumber;
            int pageSize = msgContext.Message.PageSize;
            var userId = msgContext.Message.UserId;

            var changesProductsDto = await _changesProductsService.GetPaged(page, pageSize, userId);

            await msgContext.RespondAsync(new ChangesProductsResponseDto 
            {
                ChangesProducts = changesProductsDto
            });
        }
    }
}