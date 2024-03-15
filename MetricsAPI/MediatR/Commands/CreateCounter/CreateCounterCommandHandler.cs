using MetricsAPI.Application.ApiHelpers.Responses;
using MetricsAPI.Application.Core.Abstractions.Messaging;
using MetricsAPI.Application.Core.Helpers.Metric;
using MetricsAPI.Cache.Service;
using MetricsAPI.Database.Data.Interfaces;
using MetricsAPI.Domain.Common.Core.Primitives;
using MetricsAPI.Domain.Common.Core.Primitives.Result;
using MetricsAPI.Domain.Common.Enumerations;
using Microsoft.Extensions.Caching.Distributed;
using Prometheus;

namespace MetricsAPI.MediatR.Commands.CreateCounter;

/// <summary>
/// Represents the <see cref="CreateCounterCommand"/> handler class.
/// </summary>
public sealed class CreateCounterCommandHandler(
        ILogger<CreateCounterCommandHandler> logger,
        IMetricsRepository metricsRepository,
        IDistributedCache cache)
    : ICommandHandler<CreateCounterCommand, IBaseResponse<Result>>
{
    //TODO How creating counter ever create new counter in redis.
    
    /// <inheritdoc />
    public async Task<IBaseResponse<Result>> Handle(CreateCounterCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            logger.LogInformation($"Request for create the counter by name - {request.CounterName}.");

            var counters = await metricsRepository
                .GetMetricsByTime(request.CounterName, -5);

            if (counters.Value.Count is 0)
            {
                Counter counter =
                    Metrics.CreateCounter(request.CounterName, request.CounterDescription);
                
                await cache.SetRecordAsync(
                    counter.Name,
                    counter,
                    TimeSpan.FromMinutes(6),
                    TimeSpan.FromMinutes(6));
            }
            
            logger.LogInformation($"Counter created - {request.CounterName} {DateTime.UtcNow}.");

            return new BaseResponse<Result>
            {
                Data = Result.Success(),
                Description = "Create counter.",
                StatusCode = StatusCode.Ok
            };
        }
        catch (Exception exception)
        {
            logger.LogWarning($"[CreateCounterCommandHandler]: {exception.Message}");

            return new BaseResponse<Result>
            {
                Data =
                    Result.Failure(new Error("500", exception.Message)),
                Description = exception.Message,
                StatusCode = StatusCode.InternalServerError
            };

        }
    }
}