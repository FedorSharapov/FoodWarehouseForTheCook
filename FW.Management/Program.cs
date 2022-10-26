using Serilog;
using FW.EntityFramework;
using FW.Management.Configurations;

try
{
    int SleepStart = 5;
    Console.WriteLine($"Waiting start RabbitMQ for {SleepStart} sec...");
    Thread.Sleep(TimeSpan.FromSeconds(SleepStart));

    var builder = WebApplication.CreateBuilder(args);
    builder.Environment.ApplicationName = typeof(Program).Assembly.FullName;

    builder.Services.ConfigureLogger();
    builder.Services.ConfigureMapper();
    builder.Services.AddDbContext<ApplicationContext>();
    builder.Services.ConfigureRabbitMQ();
    builder.Services.ConfigureEventHandlers();
    builder.Services.ConfigureMassTransit(builder.Configuration);
    builder.Services.AddServices();

    var app = WebApplicationConfiguration.Configure(builder);
    Log.Logger.Information($"The {app.Environment.ApplicationName} started...");
    app.Run();

    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly!");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}
