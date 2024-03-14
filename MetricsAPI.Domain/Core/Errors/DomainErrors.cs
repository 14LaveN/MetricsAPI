using MetricsAPI.Domain.Common.Core.Primitives;

namespace MetricsAPI.Domain.Core.Errors;

/// <summary>
/// Contains the domain errors.
/// </summary>
public static class DomainErrors
{
    /// <summary>
    /// Contains the message errors.
    /// </summary>
    public static class Metrics
    {
        public static Error NotFound =>
            new("Metrics.NotFound", "The metrics with the specified name was not found.");
    }
    
    /// <summary>
    /// Contains the name errors.
    /// </summary>
    public static class Name
    {
        public static Error NullOrEmpty => new("Name.NullOrEmpty", "The name is required.");

        public static Error LongerThanAllowed => new("Name.LongerThanAllowed", "The name is longer than allowed.");
    }
    
    ///<summary>
    /// Contains general errors.
    ///</summary>
    public static class General
    {
        public static Error UnProcessableRequest => new(
            "General.UnProcessableRequest",
            "The server could not process the request.");

        public static Error ServerError => new("General.ServerError", "The server encountered an unrecoverable error.");
    }
}