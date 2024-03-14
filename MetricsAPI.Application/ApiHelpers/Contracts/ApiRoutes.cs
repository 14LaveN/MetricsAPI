namespace MetricsAPI.Application.ApiHelpers.Contracts;

/// <summary>
/// Contains the API endpoint routes.
/// </summary>
public static class ApiRoutes
{
    /// <summary>
    /// Contains the task routes.
    /// </summary>
    public static class Task
    {
        public const string Create = "create-task";

        public const string DoneTask = "donetask/{taskId:guid}";

        public const string GetAuthorTasksByIsDone = "get-authror_tasks-by-is_done";
        
        public const string GetTaskById = "get-task/{taskId:guid}";
        
        public const string Update = "update-task/{taskId:guid}";
    }
}