using MetricsAPI.Application.Core.Abstractions.Messaging;
using MetricsAPI.Contracts.Metrics.GetMetric;
using MetricsAPI.Domain.Common.Core.Primitives.Maybe;
using MetricsAPI.Domain.Common.ValueObjects;

namespace MetricsAPI.MediatR.Queries.GetMetricsByNameInTime;

/// <summary>
/// Represents the get metrics by name in some time record.
/// </summary>
/// <param name="MetricName">The metric name.</param>
/// <param name="Time">The time.</param>
public sealed record GetMetricsByNameInTimeQuery(
    Name MetricName,
    int Time)
    : ICachedQuery<Maybe<List<MetricResponse>>>
{
    /// <inheritdoc/>
    public string Key { get; } = 
        $"get-metrics_by-{MetricName.Value}_in-{Time}.";
    
    /// <inheritdoc/>
    public TimeSpan? Expiration { get; } = TimeSpan.FromMinutes(5);
}