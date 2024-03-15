using System.Diagnostics.Metrics;
using MetricsAPI.Domain.Entities;

namespace MetricsAPI.Contracts.Metrics.GetMetric;

/// <summary>
/// Represents the <see cref="MetricEntity"/> request record.
/// </summary>
/// <param name="MetricName">The <see cref="MetricEntity"/> name.</param>
/// <param name="Time">The time.</param>
public sealed record MetricRequest(
    string MetricName,
    int Time);