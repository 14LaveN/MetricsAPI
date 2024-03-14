using System.Diagnostics.Metrics;
using MetricsAPI.Domain.Entities;

namespace MetricsAPI.Contracts.Metrics.GetMetric;

/// <summary>
/// Represents the create counter request record.
/// </summary>
/// <param name="CounterName">The <see cref="Counter{T}"/> name.</param>
/// <param name="CounterDescription">The <see cref="Counter{T}"/> description.</param>
public sealed record MetricResponse(
    string CounterName,
    string CounterDescription)
{
    /// <summary>
    /// Create new instance of metric entity by <see cref="MetricEntity"/>.
    /// </summary>
    /// <param name="response">The metric response.</param>
    /// <returns>Returns new <see cref="MetricResponse"/>.</returns>
    public static implicit operator MetricResponse(MetricEntity response) =>
        new(
            response.Name,
            response.Description);
};