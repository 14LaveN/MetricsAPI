using MetricsAPI.Domain.Common.Core.Primitives.Result;
using MetricsAPI.Domain.Common.Enumerations;

namespace MetricsAPI.Application.ApiHelpers.Responses;

/// <summary>
/// Represents the base response class.
/// </summary>
/// <typeparam name="T">The generic result class.</typeparam>
public class BaseResponse<T> : IBaseResponse<T>
    where T : Result
{
    /// <inheritdoc />
    public required string Description { get; set; }

    /// <inheritdoc />
    public Task<T> Data { get; set; }

    /// <inheritdoc />
    public required StatusCode StatusCode { get; set; }
}