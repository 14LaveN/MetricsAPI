using System.ComponentModel.DataAnnotations;
using MetricsAPI.Domain.Common.Core.Primitives;
using MetricsAPI.Domain.Core.Utility;

namespace MetricsAPI.Domain.Entities;

/// <summary>
/// Represents metric entity class.
/// </summary>
public sealed class MetricEntity : Entity
{
    /// <summary>
    /// Initialize the <see cref="MetricEntity"/> class.
    /// </summary>
    /// <param name="name">The metric name.</param>
    /// <param name="description">The metric Description.</param>
    public MetricEntity(string name, string description)
    {
        Ensure.NotEmpty(name, "The name is required.", nameof(name));
        Ensure.NotEmpty(description, "The description is required.", nameof(description));
        
        Name = name;
        Description = description;
    }
    
    /// <summary>
    /// Gets or sets metric name.
    /// </summary>
    [Required]
    public string Name { get; set; }
    
    /// <summary>
    /// Gets or sets metric description.
    /// </summary>
    [Required]
    public string Description { get; set; }
}