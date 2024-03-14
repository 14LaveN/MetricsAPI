using System.Diagnostics.Metrics;

namespace MetricsAPI.Contracts.Metrics.CreateMetric;

/// <summary>
/// Represents the create histogram request record.
/// </summary>
/// <param name="HistogramName">The <see cref="Histogram{T}"/> name.</param>
/// <param name="HistogramDescription">The <see cref="Histogram{T}"/> description.</param>
public sealed record CreateHistogramRequest(
    string HistogramName,
    string HistogramDescription);