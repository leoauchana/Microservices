using Domain.Interfaces;
using Infraestructure.Configurations;
using Infraestructure.Data.Repository;
using Infraestructure.Messaging.RabbitMq.Configurations;
using Infraestructure.Messaging.RabbitMq.Connections;
using Infraestructure.Messaging.RabbitMq.Consumers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infraestructure;

public static class ServiceExtensions
{
    public static void AddInfraestructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<DatabaseOptions>()
            .Bind(configuration.GetSection(DatabaseOptions.Section))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        services.AddOptions<RabbitMqOptions>()
            .Bind(configuration.GetSection(RabbitMqOptions.Section))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        services.AddScoped<IRepository, Repository>();
        services.AddSingleton<RabbitMqConnection>();
        services.AddHostedService<OrderCreatedConsumer>();

        services.AddDbContext<Data.Context.ProductServiceContext>((opt, conf) =>
        {
            var dbOptions = opt.GetRequiredService<IOptions<DatabaseOptions>>().Value;
            conf.UseNpgsql(dbOptions.Db_Product);
        });
    }
}
