using MediatR;
using MetricsAPI.Application.ApiHelpers.Infrastructure;
using MetricsAPI.Domain.Entities;
using Microsoft.AspNetCore.Components;

namespace MetricsAPI.Controllers.V1;

/// <summary>
/// Represents the <see cref="MetricEntity"/> controller.
/// </summary>
///
[Route("api/v1/metrics")]
public sealed class MetricsController(ISender sender)
    : ApiController(sender, nameof(MetricsController))
{
    
}