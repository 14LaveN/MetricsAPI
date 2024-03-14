using MetricsAPI.Application.ApiHelpers.Responses;
using MetricsAPI.Application.Core.Abstractions.Messaging;
using MetricsAPI.Domain.Common.Core.Primitives.Result;
using MetricsAPI.Domain.Common.ValueObjects;
using Prometheus;

namespace MetricsAPI.MediatR.Commands.CreateHistogram;

/// <summary>
/// Represents the create histogram command record class.
/// </summary>
/// <param name="HistogramName">The <see cref="Histogram"/> name.</param>>
/// <param name="HistogramDescription">The <see cref="Histogram"/> description.</param>>
public sealed record CreateHistogramCommand(
        Name HistogramName,
        string HistogramDescription)
    : ICommand<IBaseResponse<Result>>;