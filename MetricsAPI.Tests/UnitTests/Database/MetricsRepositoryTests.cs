using AutoFixture;
using MetricsAPI.Application.Core.Settings;
using MetricsAPI.Database.Data.Interfaces;
using MetricsAPI.Database.Data.Repositories;
using MetricsAPI.Domain.Entities;

namespace MetricsAPI.Tests.UnitTests.Database;

public sealed class MetricsRepositoryTests
{
    private readonly IMetricsRepository _metricsRepository = new MetricsRepository(new MongoSettings());
    
    [Fact]
    public async Task InsertMetric_AssertWithOk()
    {
        var fixture = new Fixture();
        var metric = fixture.Create<MetricEntity>();
        
        metric.Id = string.Empty;

        await _metricsRepository.InsertAsync(metric);
    }
    
    [Fact]
    public async Task InsertRangeMetrics_AssertWithOk()
    {
        var fixture = new Fixture();
        
        var firstMetric = fixture.Create<MetricEntity>();
        var secondMetric = fixture.Create<MetricEntity>();
        
        firstMetric.Id = string.Empty;
        secondMetric.Id = string.Empty;

        await _metricsRepository.InsertRangeAsync(new List<MetricEntity>{firstMetric,secondMetric});
    }
    
    [Fact]
    public async Task GetMetricsByTime_AssertWithNotNull()
    {
        var fixture = new Fixture();
        var metricTime = fixture.Create<int>();

        var result = await _metricsRepository.GetMetricsByTime("Name6893d8d8-f97e-4aee-ac79-a4c674859be0",metricTime);

        result.Value.Should().NotBeNull();
        result.Value.Should().NotBeNullOrEmpty();
    }
}