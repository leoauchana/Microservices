using Data.Context;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Transversal.Configurations;

namespace Data;

public static class ServiceExtensions
{
    public static void AddDataServices(this IServiceCollection services)
    {
        services.AddScoped<IRepository, Repository.Repository>();
        services.AddDbContext<OrderServiceContext>((opt, conf) =>
        {
            var dbOption = opt.GetRequiredService<IOptions<DatabaseOptions>>().Value;
            conf.UseNpgsql(dbOption.Db_Order);
        });
    }
}
