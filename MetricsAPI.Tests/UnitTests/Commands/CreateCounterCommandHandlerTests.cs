using AutoFixture;
using MetricsAPI.Application.ApiHelpers.Responses;
using MetricsAPI.Application.Core.Settings;
using MetricsAPI.Database.Data.Repositories;
using MetricsAPI.Domain.Common.Core.Primitives;
using MetricsAPI.Domain.Common.Core.Primitives.Result;
using MetricsAPI.Domain.Common.Enumerations;
using MetricsAPI.MediatR.Commands.CreateCounter;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace MetricsAPI.Tests.UnitTests.Commands;

public sealed class CreateCounterCommandHandlerTests
{
    private readonly CreateCounterCommandHandler _handler = 
        new(new Mock<ILogger<CreateCounterCommandHandler>>().Object,
        new MetricsRepository(new MongoSettings()),
        new Mock<IDistributedCache>().Object);
    
    [Fact]
    public async Task CreateCounterCommandHandle_AssertWithOk()
    {
        var fixture = new Fixture();
        var metricNumber = fixture.Create<int>();
        var command = fixture.Create<CreateCounterCommand>();

        command.CounterName.Value = $"metric_{metricNumber}";

        var result = await _handler.Handle(command);

        result.StatusCode.Should().NotBe(StatusCode.InternalServerError);
        result.Description.Should().Be("Create counter.");
    }
    
    [Fact]
    public async Task GetMetricsByNameInTimeQueryHandle_AssertWithOk()
    {
        var fixture = new Fixture();
        var metricNumber = fixture.Create<int>();
        var command = fixture.Create<CreateCounterCommand>();

        command.CounterName.Value = $"metric_{metricNumber}";

        var result = await _handler.Handle(command);

        result.StatusCode.Should().NotBe(StatusCode.InternalServerError);
        result.Description.Should().Be("Create counter.");
    }
}