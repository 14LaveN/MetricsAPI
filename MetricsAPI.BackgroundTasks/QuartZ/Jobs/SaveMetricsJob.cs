using System.Diagnostics.Metrics;
using System.Globalization;
using MetricsAPI.Application.Core.Settings;
using MetricsAPI.Cache.Service;
using MetricsAPI.Domain.Common.Core.Exceptions;
using MetricsAPI.Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Prometheus;
using Quartz;
using Quartz.Util;

namespace MetricsAPI.BackgroundTasks.QuartZ.Jobs;

/// <summary>
/// Represents the save metrics job class.
/// </summary>
public sealed class SaveMetricsJob
    : IJob
{
    private readonly IMongoCollection<MetricEntity> _metricsCollection;
    private readonly IDistributedCache _distributedCache;
    private readonly ILogger<SaveMetricsJob> _logger;

    /// <summary>
    /// Initialize a new instance of the <see cref="SaveMetricsJob"/>
    /// </summary>
    /// <param name="dbSettings">The mongo db settings.</param>
    /// <param name="distributedCache">The distributed cache.</param>
    /// <param name="logger">The logger.</param>
    public SaveMetricsJob(IOptions<MongoSettings> dbSettings,
        IDistributedCache distributedCache,
        ILogger<SaveMetricsJob> logger)
    {
        _distributedCache = distributedCache;
        _logger = logger;

        var mongoClient = new MongoClient(
            dbSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            dbSettings.Value.Database);

        _metricsCollection = mongoDatabase.GetCollection<MetricEntity>(
            dbSettings.Value.MetricsCollectionName);
    }
    
    /// <inheritdoc/>
    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            _logger.LogInformation($"Request to save metrics in {nameof(SaveMetricsJob)}.");

            string counterName = "metrics_counter-key";
            
            Counter counter = await _distributedCache
                .GetRecordAsync<Counter>(counterName);

            string histogramName = "metrics_request_duration_seconds-key";
            
            string millisecondsInString =
                await _distributedCache.GetRecordAsync<string>(histogramName);
            
            if (counter is null )
            {
                _logger.LogWarning($"Counter with same name - {counterName} not found");
                throw new NotFoundException(nameof(Counter), counterName);
            }

            if (millisecondsInString.IsNullOrWhiteSpace())
            {
                _logger.LogWarning($"Histogram with same name - {histogramName} not found");
                throw new NotFoundException("Milliseconds", histogramName);
            }
            
            var metrics = new List<MetricEntity>
            { 
                new("AspNetNetwork_request_duration_seconds",
                    millisecondsInString),
                new(counter.Name,
                    counter.Value.ToString(CultureInfo.CurrentCulture))
            };
            
            await _metricsCollection.InsertManyAsync(metrics);

            _logger.LogInformation($"Insert in MongoDb metrics at - {DateTime.UtcNow}");
        }
        catch (Exception exception)
        {
            _logger.LogError($"[SaveMetricsJob]: {exception.Message}");
            throw;
        }
    }
}