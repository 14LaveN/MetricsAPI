using MetricsAPI.Application.Core.Settings;
using MetricsAPI.Database.Data.Repositories;
using MetricsAPI.Domain.Common.Enumerations;
using MetricsAPI.MediatR.Commands.CreateCounter;
using MetricsAPI.MediatR.Queries.GetMetricsByNameInTime;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace MetricsAPI.Tests.UnitTests.Commands;

public sealed class GetMetricsByNameInTimeQueryHandlerTests
{
    private readonly GetMetricsByNameInTimeQueryHandler _handler =
        new(new Mock<ILogger<GetMetricsByNameInTimeQueryHandler>>().Object,
            new MetricsRepository(new MongoSettings()));
    
    [Fact]
    public async Task GetMetricsByNameInTimeQueryHandle_AssertWithOk()
    {
        var fixture = new Fixture();
        var query = fixture.Create<GetMetricsByNameInTimeQuery>();

        query.MetricName.Value = "Namef4695aeb-c4ad-4d7b-aa62-beeab0b2f53e";

        var result = await _handler.Handle(query);

        result.HasValue.Should().NotBe(false);
        result.Value.Should().NotBeNull();
    }
}