using MediatR;
using MediatR.NotificationPublishers;
using MetricsAPI.Application.Core.Behaviours;
using MetricsAPI.MediatR.Commands.CreateCounter;
using MetricsAPI.MediatR.Commands.CreateHistogram;
using MetricsAPI.MediatR.Queries.GetMetricsByNameInTime;

namespace MetricsAPI.Common.DependencyInjection;

/// <summary>
/// Represents the dependency injection container for <see cref="MediatR"/>.
/// </summary>
internal static class DiMediator
{
    /// <summary>
    /// Registers the necessary services with the DI framework.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The same service collection.</returns>
    public static IServiceCollection AddMediatr(this IServiceCollection services)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddMediatR(x =>
        {
            x.RegisterServicesFromAssemblyContaining<Program>();

            x.RegisterServicesFromAssemblies(typeof(CreateCounterCommand).Assembly,
                typeof(CreateCounterCommandHandler).Assembly);
            
            x.RegisterServicesFromAssemblies(typeof(CreateHistogramCommand).Assembly,
                typeof(CreateHistogramCommandHandler).Assembly);
            
            x.RegisterServicesFromAssemblies(typeof(GetMetricsByNameInTimeQuery).Assembly,
                typeof(GetMetricsByNameInTimeQueryHandler).Assembly);
            
            x.NotificationPublisher = new TaskWhenAllPublisher();
            x.NotificationPublisherType = typeof(TaskWhenAllPublisher);
            
            x.Lifetime = ServiceLifetime.Transient;
            
            x.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            x.AddBehavior(typeof(IPipelineBehavior<,>), typeof(MetricsBehaviour<,>));
            x.AddOpenBehavior(typeof(QueryCachingBehavior<,>));
        });
        
        return services;
    }
}