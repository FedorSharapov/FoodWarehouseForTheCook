using MassTransit;
using FW.BusinessLogic.Contracts;
using FW.BusinessLogic.Contracts.Category;
using FW.BusinessLogic.Services.Abstractions;
using MassTransit.ConsumeConfigurators;


namespace FW.Management.Consumers.CategoriesConsumer
{
    public class CategoriesGetCountConsumer : IConsumer<CategoriesGetCountDto>
    {
        private readonly ILogger _logger;
        private readonly ICategoriesService _categoriesService;

        public CategoriesGetCountConsumer(ILogger logger, ICategoriesService categoriesService)
        {
            _logger = logger;
            _categoriesService = categoriesService;
        }

        public async Task Consume(ConsumeContext<CategoriesGetCountDto> msgContext)
        {
            _logger.Information($"Received a message from the {msgContext.SourceAddress}");

            var userId = msgContext.Message.UserId;
            var count = await _categoriesService.Count(userId);

            await msgContext.RespondAsync<CountResponseDto>(new CountResponseDto
            { 
                Count = count
            });
        }
    }
}