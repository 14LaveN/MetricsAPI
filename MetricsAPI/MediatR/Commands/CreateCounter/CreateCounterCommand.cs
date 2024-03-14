using MetricsAPI.Application.ApiHelpers.Responses;
using MetricsAPI.Application.Core.Abstractions.Messaging;
using MetricsAPI.Domain.Common.Core.Primitives.Result;
using MetricsAPI.Domain.Common.ValueObjects;
using Prometheus;

namespace MetricsAPI.MediatR.Commands.CreateCounter;

/// <summary>
/// Represents the create counter command record class.
/// </summary>
/// <param name="CounterName">The <see cref="Counter"/> name.</param>>
/// <param name="CounterDescription">The <see cref="Counter"/> description.</param>>
public sealed record CreateCounterCommand(
    Name CounterName,
    string CounterDescription)
    : ICommand<IBaseResponse<Result>>;