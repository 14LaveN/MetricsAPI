using MetricsAPI.Database;

namespace MetricsAPI.Common.DependencyInjection;

/// <summary>
/// Represents the dependency injection container for database.
/// </summary>
internal static class DiDatabase
{
    /// <summary>
    /// Registers the necessary services with the DI framework.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>The same service collection.</returns>
    public static IServiceCollection AddDatabase(this IServiceCollection services,
        IConfiguration configuration)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddMongoDatabase(configuration);
        
        return services;
    }
}