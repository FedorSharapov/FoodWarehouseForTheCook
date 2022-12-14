using FW.BusinessLogic.Contracts;
using FW.BusinessLogic.Contracts.Warehouses;
using FW.BusinessLogic.Services.Abstractions;
using FW.EventBus;
using FW.EventBus.Interfaces;

namespace FW.Management.EventHandlers.Warehouses
{
    public class WarehouseUpdateEventHandler : IIntegrationEventHandler<WarehouseUpdateDto>
    {
        private readonly ILogger _logger;
        private readonly IWarehousesService _warehousesService;

        public WarehouseUpdateEventHandler(ILogger logger, IWarehousesService warehousesService)
        {
            _logger = logger;
            _warehousesService = warehousesService;
        }

        public async Task Handle(IntegrationContext<WarehouseUpdateDto> msgContext)
        {
            _logger.Information($"Received a message from exchange/queue: {msgContext.ExchangeName}/{msgContext.QueueName}");

            var warehouseDto = msgContext.Message;
            var status = await _warehousesService.Update(warehouseDto);

            if (status)
                await msgContext.RespondAsync(new ResponseStatusResultWithoutId
                {
                    Status = StatusResult.Ok,
                    Title = "Updated"
                });
            else
                await msgContext.RespondAsync(new ResponseStatusResultWithoutId
                {
                    Status = StatusResult.NotFound,
                    Title = "Not found"
                });
        }
    }
}
