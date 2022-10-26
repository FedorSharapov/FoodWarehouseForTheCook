using MassTransit;
using FW.BusinessLogic.Contracts.Category;
using FW.BusinessLogic.Services.Abstractions;
using MassTransit.ConsumeConfigurators;

namespace FW.Management.Consumers.CategoriesConsumer
{
    public class CategoriesGetPageConsumer : IConsumer<CategoriesGetPageDto>
    {
        private readonly ILogger _logger;
        private readonly ICategoriesService _categoriesService;

        public CategoriesGetPageConsumer(ILogger logger, ICategoriesService categoriesService)
        {
            _logger = logger;
            _categoriesService = categoriesService;
        }

        public async Task Consume(ConsumeContext<CategoriesGetPageDto> msgContext)
        {
            _logger.Information($"Received a message from the {msgContext.SourceAddress}");

            int page = msgContext.Message.PageNumber;
            int pageSize = msgContext.Message.PageSize;
            var userId = msgContext.Message.UserId;

            var categoriesDto = await _categoriesService.GetPaged(page, pageSize, userId);

            await msgContext.RespondAsync<CategoriesResponseDto>(new CategoriesResponseDto
            { 
                Categories = categoriesDto 
            });
        }
    }
}