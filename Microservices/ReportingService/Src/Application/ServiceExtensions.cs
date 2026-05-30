using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ServiceExtensions
{
    public static void AddApplicationServices(
        this IServiceCollection services)
    {
        services.AddScoped<IReportingService, ReportingService>();
    }
}
