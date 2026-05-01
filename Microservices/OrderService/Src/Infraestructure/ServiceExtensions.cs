using Domain.Interfaces;
using Infraestructure.Configurations.Options;
using Infraestructure.Data.Context;
using Infraestructure.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infraestructure;

public static class ServiceExtensions
{
    public static void AddInfraestructureServices(this IServiceCollection services)
    {
        services.AddScoped<IRepository, Repository>();
        services.AddDbContext<OrderServiceContext>((opt, conf) =>
        {
            var dbOptions = opt.GetRequiredService<IOptions<DatabaseOptions>>().Value;
            conf.UseNpgsql(dbOptions.Db_Order);
        });
    }
}
