using MetricsAPI.Domain.Common.Core.Primitives;

namespace MetricsAPI.Application.Core.Errors;

/// <summary>
/// Contains the validation errors.
/// </summary>
public static class ValidationErrors
{
    /// <summary>
    /// Contains the create counter errors.
    /// </summary>
    public static class CreateCounter
    {
        public static Error DescriptionIsRequired => 
            new("CreateCounter.DescriptionIsRequired", "The description is required.");

        public static Error NameIsRequired => 
            new("CreateCounter.NameIsRequired", "The name is required.");
    }
    
    /// <summary>
    /// Contains the create histogram errors.
    /// </summary>
    public static class CreateHistogram
    {
        public static Error DescriptionIsRequired => 
            new("CreateHistogram.DescriptionIsRequired", "The description is required.");

        public static Error NameIsRequired => 
            new("CreateHistogram.NameIsRequired", "The name is required.");
    }
}