using Infraestructure.Messaging.RabbitMq.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Infraestructure.Messaging.RabbitMq.Connection;
using Application.Interfaces.Repositories;
using Infraestructure.Data.Configurations;
using Infraestructure.Data.Persistence;
using Infraestructure.Data.Repository;
using Infraestructure.Messaging.RabbitMq.Consumers;
namespace Infraestructure;

public static class ServiceExtensions
{
    public static void AddInfraestructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOptions<RabbitMqOptions>()
        .Bind(configuration.GetSection(RabbitMqOptions.Section))
        .ValidateDataAnnotations()
        .ValidateOnStart();
        services.AddOptions<DatabaseOptions>()
        .Bind(configuration.GetSection(DatabaseOptions.Section))
        .ValidateDataAnnotations()
        .ValidateOnStart();
        services.AddSingleton<RabbitMqConnection>();
        services.AddSingleton<IDbConnectionFactory, NpgSqlConnectionFactory>();
        services.AddScoped<IReportRepository, ReportRepository>();
        services.AddHostedService<OrderCreatedConsumer>();
        services.AddHostedService<ProductCreatedConsumer>();
    }
}