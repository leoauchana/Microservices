using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Data;

public static class ServiceExtensions
{
    public static void AddDataServices(this IServiceCollection services)
    {
        services.AddScoped<IRepository, Repository.Repository>();
    }
}
