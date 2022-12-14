using FW.Management.Consumers.CategoriesConsumer;
using FW.Management.Consumers.ProductsConsumers;
using FW.RabbitMQOptions;
using MassTransit;
using MassTransit.Definition;

namespace FW.Management.Configurations
{
    public static class MassTransitConfiguration
    {
        public static void ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMqOptions = configuration.GetSection(RabbitMqConnectionOptions.KeyValue).Get<RabbitMqConnectionOptions>();

            services.AddMassTransit(x =>
            {
                x.SetSnakeCaseEndpointNameFormatter();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host($"rabbitmq://{rabbitMqOptions.HostName}/{rabbitMqOptions.VirtualHost}", hostCfg =>
                    {
                        hostCfg.Username(rabbitMqOptions.UserName);
                        hostCfg.Password(rabbitMqOptions.Password);
                    });

                    cfg.ClearMessageDeserializers();
                    cfg.UseRawJsonSerializer();
                    cfg.ConfigureEndpoints(context, SnakeCaseEndpointNameFormatter.Instance);
                });

                x.AddConsumer<ProductCreateConsumer>();
                x.AddConsumer<ProductGetByIdConsumer>();
                x.AddConsumer<ProductUpdateConsumer>();
                x.AddConsumer<ProductDeleteConsumer>();
                x.AddConsumer<ProductsGetPageConsumer>();
                x.AddConsumer<ProductsGetAllConsumer>();
                x.AddConsumer<ProductsGetCountConsumer>();

                x.AddConsumer<CategoryCreateConsumer>();
                x.AddConsumer<CategoryGetByIdConsumer>();
                x.AddConsumer<CategoryUpdateConsumer>();
                x.AddConsumer<CategoryDeleteConsumer>();
                x.AddConsumer<CategoriesGetPageConsumer>();
                x.AddConsumer<CategoriesGetAllConsumer, CategoriesGetAllConsumerDefenition>();
                x.AddConsumer<CategoriesGetCountConsumer>();
            });

            services.AddMassTransitHostedService();
        }
    }
}
