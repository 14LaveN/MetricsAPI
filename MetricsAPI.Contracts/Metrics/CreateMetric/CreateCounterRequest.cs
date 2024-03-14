using System.Diagnostics.Metrics;

namespace MetricsAPI.Contracts.Metrics.CreateMetric;

/// <summary>
/// Represents the create counter request record.
/// </summary>
/// <param name="CounterName">The <see cref="Counter{T}"/> name.</param>
/// <param name="CounterDescription">The <see cref="Counter{T}"/> description.</param>
public sealed record CreateCounterRequest(
    string CounterName,
    string CounterDescription);