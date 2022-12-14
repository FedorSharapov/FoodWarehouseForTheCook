using MassTransit;
using FW.BusinessLogic.Contracts.Category;
using FW.BusinessLogic.Services.Abstractions;

using MassTransit.Definition;
using MassTransit.ConsumeConfigurators;
using GreenPipes;

namespace FW.Management.Consumers.CategoriesConsumer
{
    public class CategoriesGetAllConsumer : IConsumer<CategoriesGetAllDto>
    {
        private readonly ILogger _logger;
        private readonly ICategoriesService _categoriesService;

        public CategoriesGetAllConsumer(ILogger logger, ICategoriesService categoriesService)
        {
            _logger = logger;
            _categoriesService = categoriesService;
        }

        public async Task Consume(ConsumeContext<CategoriesGetAllDto> msgContext)
        {
            _logger.Information($"Received a message from the {msgContext.SourceAddress}");

            var userId = msgContext.Message.UserId;
            var categoriesDto = await _categoriesService.GetAll(userId);

            await msgContext.RespondAsync<CategoriesResponseDto>(new CategoriesResponseDto
            {
                Categories = categoriesDto 
            });
        }
    }

    // Пример изменения настроек Consumer
    public class CategoriesGetAllConsumerDefenition : ConsumerDefinition<CategoriesGetAllConsumer>
    {
        public CategoriesGetAllConsumerDefenition()
        {
            //EndpointName = MassTransitQueueName.Category.Create;    // имя очереди
            ConcurrentMessageLimit = 1;                            // ограничение количества сообщений, потребляемых одновременно
        }

        // изменение конфигурации
        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<CategoriesGetAllConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r => r.Intervals(100, 200, 500, 800, 1000));
            endpointConfigurator.UseTimeout(x => x.Timeout = TimeSpan.FromSeconds(60));
        }
    }
}