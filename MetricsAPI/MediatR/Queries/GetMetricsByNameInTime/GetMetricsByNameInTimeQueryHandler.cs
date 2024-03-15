using MetricsAPI.Application.Core.Abstractions.Messaging;
using MetricsAPI.Contracts.Metrics.GetMetric;
using MetricsAPI.Database.Data.Interfaces;
using MetricsAPI.Domain.Common.Core.Exceptions;
using MetricsAPI.Domain.Common.Core.Primitives.Maybe;
using MetricsAPI.Domain.Core.Errors;
using Microsoft.Extensions.Caching.Distributed;

namespace MetricsAPI.MediatR.Queries.GetMetricsByNameInTime;

/// <summary>
/// Represents the <see cref="GetMetricsByNameInTimeQuery"/> handler class.
/// </summary>
public sealed class GetMetricsByNameInTimeQueryHandler(
    ILogger<GetMetricsByNameInTimeQueryHandler> logger,
    IMetricsRepository metricsRepository)
    : IQueryHandler<GetMetricsByNameInTimeQuery, Maybe<List<MetricResponse>>>
{
    /// <inheritdoc />
    public async Task<Maybe<List<MetricResponse>>> Handle(
        GetMetricsByNameInTimeQuery request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            logger.LogInformation($"Request for get metrics by name - {request.MetricName} in time - {request.Time}.");

            var metricsInTime = await metricsRepository
                .GetMetricsByTime(request.MetricName, request.Time);

            if (metricsInTime.HasNoValue)
            {
                logger.LogWarning($"Metrics by name - {request.MetricName} in time - {request.Time} not found.");

                throw new NotFoundException(DomainErrors.Metrics.NotFound, DomainErrors.Metrics.NotFound);
            }
            
            logger.LogInformation($"Get metrics with name - {request.MetricName} in time - {request.Time}.");

            return metricsInTime.Value.Select(index =>
                (MetricResponse)index).ToList();
        }
        catch (Exception exception)
        {
            logger.LogError(exception, $"[GetAuthorTasksByIsDoneQueryHandler]: {exception.Message}");
            throw new ApplicationException();
        }
    }
}