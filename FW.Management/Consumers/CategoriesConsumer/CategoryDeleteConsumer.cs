using MassTransit;
using FW.BusinessLogic.Contracts.Category;
using FW.BusinessLogic.Contracts;
using FW.BusinessLogic.Services.Abstractions;

namespace FW.Management.Consumers.CategoriesConsumer
{
    public class CategoryDeleteConsumer : IConsumer<CategoryDeleteDto>
    {
        private readonly ILogger _logger;
        private readonly ICategoriesService _categoriesService;

        public CategoryDeleteConsumer(ILogger logger, ICategoriesService categoriesService)
        {
            _logger = logger;
            _categoriesService = categoriesService;
        }

        public async Task Consume(ConsumeContext<CategoryDeleteDto> msgContext)
        {
            _logger.Information($"Received a message from the {msgContext.SourceAddress}");

            var categoryId = msgContext.Message.Id;
            var userId = msgContext.Message.UserId;

            var status = await _categoriesService.Delete(categoryId, userId);
            if (status)
            {
                await msgContext.RespondAsync(new ResponseStatusResult
                {
                    Id = categoryId,
                    Status = StatusResult.Ok,
                    Title = "Deleted"
                });
            }
            else
            {
                await msgContext.RespondAsync(new ResponseStatusResult
                {
                    Id = categoryId,
                    Status = StatusResult.NotFound,
                    Title = "Not found"
                });
            }
        }
    }
}