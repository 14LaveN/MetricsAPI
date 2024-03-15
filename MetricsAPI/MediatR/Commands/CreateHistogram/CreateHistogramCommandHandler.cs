using MetricsAPI.Application.ApiHelpers.Responses;
using MetricsAPI.Application.Core.Abstractions.Messaging;
using MetricsAPI.Cache.Service;
using MetricsAPI.Database.Data.Interfaces;
using MetricsAPI.Domain.Common.Core.Primitives;
using MetricsAPI.Domain.Common.Core.Primitives.Result;
using MetricsAPI.Domain.Common.Enumerations;
using MetricsAPI.MediatR.Commands.CreateCounter;
using Microsoft.Extensions.Caching.Distributed;
using Prometheus;

namespace MetricsAPI.MediatR.Commands.CreateHistogram;

/// <summary>
/// Represents the <see cref="CreateHistogramCommand"/> handler class.
/// </summary>
public sealed class CreateHistogramCommandHandler(
        ILogger<CreateHistogramCommandHandler> logger,
        IMetricsRepository metricsRepository,
        IDistributedCache cache)
    : ICommandHandler<CreateHistogramCommand, IBaseResponse<Result>>
{
    //TODO How creating counter ever create new counter in redis.
    
    /// <inheritdoc />
    public async Task<IBaseResponse<Result>> Handle(
        CreateHistogramCommand request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            logger.LogInformation($"Request for create the histogram by name - {request.HistogramName}.");

            var metricsByTime = await metricsRepository
                .GetMetricsByTime(request.HistogramName, -5);

            if (metricsByTime.Value.Count is 0)
            {
                Histogram histogram =
                    Metrics.CreateHistogram(request.HistogramName, request.HistogramDescription);
                
                await cache.SetRecordAsync(
                    histogram.Name,
                    histogram,
                    TimeSpan.FromMinutes(6),
                    TimeSpan.FromMinutes(6));
            }
            
            logger.LogInformation($"Histogram created - {request.HistogramName} {DateTime.UtcNow}.");

            return new BaseResponse<Result>
            {
                Data = Result.Success(),
                Description = "Histogram counter.",
                StatusCode = StatusCode.Ok
            };
        }
        catch (Exception exception)
        {
            logger.LogWarning($"[CreateHistogramCommandHandler]: {exception.Message}");

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