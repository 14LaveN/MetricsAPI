using MediatR;
using MetricsAPI.Application.ApiHelpers.Contracts;
using MetricsAPI.Application.ApiHelpers.Infrastructure;
using MetricsAPI.Application.ApiHelpers.Policy;
using MetricsAPI.Contracts.Metrics.CreateMetric;
using MetricsAPI.Contracts.Metrics.GetMetric;
using MetricsAPI.Domain.Common.Core.Primitives.Maybe;
using MetricsAPI.Domain.Common.Core.Primitives.Result;
using MetricsAPI.Domain.Common.ValueObjects;
using MetricsAPI.Domain.Core.Errors;
using MetricsAPI.Domain.Entities;
using MetricsAPI.MediatR.Commands.CreateCounter;
using MetricsAPI.MediatR.Queries.GetMetricsByNameInTime;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAPI.Controllers.V1;

/// <summary>
/// Represents the <see cref="MetricEntity"/> controller.
/// </summary>
[Route("api/v1/metrics")]
public class MetricsController(ISender sender)
    : ApiController(sender, nameof(MetricsController))
{
    /// <summary>
    /// Create counter.
    /// </summary>
    /// <param name="request">The <see cref="CreateCounterRequest"/> class.</param>
    /// <returns>Base information about create counter method.</returns>
    /// <remarks>
    /// Example request:
    /// </remarks>
    /// <response code="200">OK.</response>
    /// <response code="400">Bad request.</response>
    /// <response code="500">Internal server error.</response>
    [HttpPost(ApiRoutes.Metrics.CreateCounter)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCounter([FromBody] CreateCounterRequest request) =>
        await Result.Create(request, DomainErrors.General.UnProcessableRequest)
            .Map(createCounterRequest => new CreateCounterCommand(
                new Name(createCounterRequest.CounterName),
                createCounterRequest.CounterDescription))
            .Bind(command => BaseRetryPolicy.Policy.Execute(async () =>
                await Sender.Send(command)).Result.Data)
            .Match(Ok, BadRequest);
    
    /// <summary>
    /// Create histogram.
    /// </summary>
    /// <param name="request">The <see cref="CreateHistogramRequest"/> class.</param>
    /// <returns>Base information about create histogram method.</returns>
    /// <remarks>
    /// Example request:
    /// </remarks>
    /// <response code="200">OK.</response>
    /// <response code="400">Bad request.</response>
    /// <response code="500">Internal server error.</response>
    [HttpPost(ApiRoutes.Metrics.CreateHistogram)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateHistogram([FromBody] CreateHistogramRequest request) =>
        await Result.Create(request, DomainErrors.General.UnProcessableRequest)
            .Map(histogramRequest => new CreateCounterCommand(
                new Name(histogramRequest.HistogramName),
                histogramRequest.HistogramDescription))
            .Bind(command => BaseRetryPolicy.Policy.Execute(async () =>
                await Sender.Send(command)).Result.Data)
            .Match(Ok, BadRequest);
    
    //TODO  Create request get record.
    
    /// <summary>
    /// Get metrics By name in time.
    /// </summary>
    /// <returns>Base information about get metrics by name in time  method.</returns>
    /// <remarks>
    /// Example request:
    /// </remarks>
    /// <response code="200">OK.</response>
    /// <response code="400">BadRequest.</response>
    /// <response code="500">Internal server error</response>
    [HttpGet(ApiRoutes.Metrics.GetMetricsByNameInTime)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAuthorTasksByIsDone(
        string name,
        int time) =>
        await Maybe<GetMetricsByNameInTimeQuery>
            .From(new GetMetricsByNameInTimeQuery())
            .Bind<GetMetricsByNameInTimeQuery, Maybe<List<MetricResponse>>>(async query => await BaseRetryPolicy.Policy.Execute(async () =>
                await Sender.Send(query)))
            .Match(Ok, NotFound);
}