using FW.BusinessLogic.Contracts;
using FW.BusinessLogic.Contracts.Ingredients;
using FW.BusinessLogic.Services.Abstractions;
using FW.EventBus;
using FW.EventBus.Interfaces;

namespace FW.Management.EventHandlers.Ingredients
{
    public class IngredientDeleteEventHandler : IIntegrationEventHandler<IngredientDeleteDto>
    {
        private readonly ILogger _logger;
        private readonly IIngredientsService _ingredientsService;

        public IngredientDeleteEventHandler(ILogger logger, IIngredientsService ingredientsService)
        {
            _logger = logger;
            _ingredientsService = ingredientsService;
        }

        public async Task Handle(IntegrationContext<IngredientDeleteDto> msgContext)
        {
            _logger.Information($"Received a message from exchange/queue: {msgContext.ExchangeName}/{msgContext.QueueName}");

            var ingredientId = msgContext.Message.Id;
            var userId = msgContext.Message.UserId;

            var status = await _ingredientsService.Delete(ingredientId, userId);
            if (status)
            {
                await msgContext.RespondAsync(new ResponseStatusResult
                {
                    Id = ingredientId,
                    Status = StatusResult.Ok,
                    Title = "Deleted"
                });
            }
            else
            {
                await msgContext.RespondAsync(new ResponseStatusResult
                {
                    Id = ingredientId,
                    Status = StatusResult.NotFound,
                    Title = "Not found"
                });
            }
        }
    }
}
