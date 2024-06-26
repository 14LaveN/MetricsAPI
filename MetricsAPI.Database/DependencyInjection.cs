using MetricsAPI.Application.Core.Settings;
using MetricsAPI.Database.Data.Interfaces;
using MetricsAPI.Database.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MetricsAPI.Database;

public static class DependencyInjection
{
    /// <summary>
    /// Registers the necessary services with the DI framework.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>The same service collection.</returns>
    public static IServiceCollection AddMongoDatabase(this IServiceCollection services,
        IConfiguration configuration)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }
        
        services.Configure<MongoSettings>(configuration.GetSection(MongoSettings.MongoSettingsKey));

        services.AddTransient<MongoSettings>(_ => new MongoSettings());
        
        services.AddScoped<IMetricsRepository, MetricsRepository>();
        
        services.AddHealthChecks()
            .AddMongoDb(configuration.GetConnectionString("MongoConnection")!);

        return services;
    }
}