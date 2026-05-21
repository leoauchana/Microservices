using Domain.Interfaces;
using Infraestructure.Configurations.Options;
using Infraestructure.Data.Context;
using Infraestructure.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Application.Interfaces.ExternalServices;
using Infraestructure.ExternalServices.ServicesClient;
namespace Infraestructure;

public static class ServiceExtensions
{
    public static void AddInfraestructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<DatabaseOptions>()
            .Bind(configuration.GetSection(DatabaseOptions.Section))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        services.AddOptions<ServicesOptions>()
            .Bind(configuration.GetSection(ServicesOptions.Section))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        services.AddOptions<RabbitMqOptions>()
            .Bind(configuration.GetSection(RabbitMqOptions.Section))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        services.AddScoped<IRepository, Repository>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IMessagePublisher, RabbitMqPublisher>();
        services.AddSingleton<RabbitMqConnection>();
        services.AddDbContext<OrderServiceContext>((opt, conf) =>
        {
            var dbOptions = opt.GetRequiredService<IOptions<DatabaseOptions>>().Value;
            conf.UseNpgsql(dbOptions.Db_Order);
        });
        services.AddHttpClient<IUserService, UserService>((opt, client) =>
        {
            var options = opt.GetRequiredService<IOptions<ServicesOptions>>().Value;

            client.BaseAddress = new Uri(options.UserService);
        });
        services.AddHttpClient<IProductService, ProductService>((opt, client) =>
        {
            var options = opt.GetRequiredService<IOptions<ServicesOptions>>().Value;

            client.BaseAddress = new Uri(options.ProductService);
        });
    }
}
