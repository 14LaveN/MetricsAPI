using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MetricsAPI.Application.ApiHelpers.Contracts;
using MetricsAPI.Domain.Common.Core.Primitives;
using MetricsAPI.Domain.Common.Core.Primitives.Result;

namespace MetricsAPI.Application.ApiHelpers.Infrastructure;

/// <summary>
/// Represents the api controller class.
/// </summary>
[ApiController]
[Produces("application/json")]
[ApiExplorerSettings(GroupName = "v1")]
public class ApiController : Controller
{
    protected ApiController(
        ISender sender,
        string controllerName)
    {
        Sender = sender;
        ControllerName = controllerName;
    }

    protected ISender Sender { get; set; }

    protected string ControllerName { get; }
        
    /// <summary>
    /// Creates an <see cref="BadRequestObjectResult"/> that produces a <see cref="StatusCodes.Status400BadRequest"/>.
    /// response based on the specified <see cref="Result"/>.
    /// </summary>
    /// <param name="error">The error.</param>
    /// <returns>The created <see cref="BadRequestObjectResult"/> for the response.</returns>
    protected IActionResult BadRequest(Error error) => BadRequest(new ApiErrorResponse(new[] { error }));
    
    /// <summary>
    /// Creates an <see cref="BadRequestObjectResult"/> that produces a <see cref="StatusCodes.Status400BadRequest"/>.
    /// response based on the specified <see cref="Result"/>.
    /// </summary>
    /// <param name="error">The error.</param>
    /// <returns>The created <see cref="BadRequestObjectResult"/> for the response.</returns>
    protected IActionResult Unauthorized(Error error) => Unauthorized(new ApiErrorResponse(new[] { error }));

    /// <summary>
    /// Creates an <see cref="OkObjectResult"/> that produces a <see cref="StatusCodes.Status200OK"/>.
    /// </summary>
    /// <returns>The created <see cref="OkObjectResult"/> for the response.</returns>
    /// <returns></returns>
    protected new IActionResult Ok(object value) => base.Ok(value);

    /// <summary>
    /// Creates an <see cref="NotFoundResult"/> that produces a <see cref="StatusCodes.Status404NotFound"/>.
    /// </summary>
    /// <returns>The created <see cref="NotFoundResult"/> for the response.</returns>
    protected new IActionResult NotFound() => base.NotFound();
}