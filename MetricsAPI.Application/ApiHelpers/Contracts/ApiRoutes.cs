namespace MetricsAPI.Application.ApiHelpers.Contracts;

/// <summary>
/// Contains the API endpoint routes.
/// </summary>
public static class ApiRoutes
{
    /// <summary>
    /// Contains the metrics routes.
    /// </summary>
    public static class Metrics
    {
        public const string CreateCounter = "create-counter";
        
        public const string CreateHistogram = "create-histogram";
        
        public const string GetMetricsByNameInTime = "get-metrics-by-name-in-time/{name:string}/{time:int}";
    }
}