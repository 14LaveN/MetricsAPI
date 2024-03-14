using Microsoft.Extensions.DependencyInjection;
using MetricsAPI.Application.Common;
using MetricsAPI.Application.Core.Abstractions.Common;
using MetricsAPI.Application.Core.Helpers.Metric;

namespace MetricsAPI.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        if (services is null)
            throw new ArgumentException();
        
        services.AddScoped<CreateMetricsHelper>();
        services.AddScoped<IDateTime, MachineDateTime>();
        
        return services;
    }
}