namespace MetricsAPI.Application.Core.Settings;

/// <summary>
/// Represents the mongo settings class.
/// </summary>
public sealed class MongoSettings
{
    /// <summary>
    /// Gets mongo settings key.
    /// </summary>
    public static string MongoSettingsKey = "MongoConnection";

    /// <summary>
    /// Gets or sets connection string.
    /// </summary>
    public string ConnectionString { get; set; } = "mongodb://localhost:27017";

    /// <summary>
    /// Gets or sets database name.
    /// </summary>
    public string Database { get; set; } = "Metrics";

    /// <summary>
    /// Gets or sets Metrics Collection Name.
    /// </summary>
    public string MetricsCollectionName { get; set; } = "Metrics";
}