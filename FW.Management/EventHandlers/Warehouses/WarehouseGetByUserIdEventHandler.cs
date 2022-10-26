using FW.BusinessLogic.Contracts.Warehouses;
using FW.BusinessLogic.Services.Abstractions;
using FW.EventBus;
using FW.EventBus.Interfaces;

namespace FW.Management.EventHandlers.Warehouses
{
    public class WarehouseGetByUserIdEventHandler : IIntegrationEventHandler<WarehouseGetByUserIdDto>
    {
        private readonly ILogger _logger;
        private readonly IWarehousesService _warehousesService;

        public WarehouseGetByUserIdEventHandler(ILogger logger, IWarehousesService warehousesService)
        {
            _logger = logger;
            _warehousesService = warehousesService;
        }

        public async Task Handle(IntegrationContext<WarehouseGetByUserIdDto> msgContext)
        {
            _logger.Information($"Received a message from exchange/queue: {msgContext.ExchangeName}/{msgContext.QueueName}");

            var userId = msgContext.Message.UserId;
            var warehouseDto = await _warehousesService.GetByUserId(userId);

            if (warehouseDto != null)
                await msgContext.RespondAsync(warehouseDto);
            else
                await msgContext.RespondAsync(new WarehouseResponseDto {});
        }
    }
}
