using FW.BusinessLogic.Contracts.Recipes;
using FW.BusinessLogic.Services.Abstractions;
using FW.EventBus;
using FW.EventBus.Interfaces;

namespace FW.Management.EventHandlers.Recipes
{
    public class RecipesGetPageEventHandler : IIntegrationEventHandler<RecipesGetPageDto>
    {
        private readonly ILogger _logger;
        private readonly IRecipesService _recipesService;

        public RecipesGetPageEventHandler(ILogger logger, IRecipesService recipesService)
        {
            _logger = logger;
            _recipesService = recipesService;
        }

        public async Task Handle(IntegrationContext<RecipesGetPageDto> msgContext)
        {
            _logger.Information($"Received a message from exchange/queue: {msgContext.ExchangeName}/{msgContext.QueueName}");

            int page = msgContext.Message.PageNumber;
            int pageSize = msgContext.Message.PageSize;
            var userId = msgContext.Message.UserId;

            var recipesDto = await _recipesService.GetPaged(page, pageSize, userId);

            await msgContext.RespondAsync(new RecipesResponseDto 
            {
                Recipes = recipesDto
            });
        }
    }
}
