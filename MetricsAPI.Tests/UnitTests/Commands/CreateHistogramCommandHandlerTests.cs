using MetricsAPI.Application.Core.Settings;
using MetricsAPI.Database.Data.Repositories;
using MetricsAPI.Domain.Common.Enumerations;
using MetricsAPI.MediatR.Commands.CreateCounter;
using MetricsAPI.MediatR.Commands.CreateHistogram;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace MetricsAPI.Tests.UnitTests.Commands;

public sealed class CreateHistogramCommandHandlerTests
{
    private readonly CreateHistogramCommandHandler _handler = 
        new(new Mock<ILogger<CreateHistogramCommandHandler>>().Object,
            new MetricsRepository(new MongoSettings()),
            new Mock<IDistributedCache>().Object);
    
    [Fact]
    public async Task CreateHistogramCommandHandle_AssertWithOk()
    {
        var fixture = new Fixture();
        var metricNumber = fixture.Create<int>();
        var command = fixture.Create<CreateHistogramCommand>();

        command.HistogramName.Value = $"metric_{metricNumber}";

        var result = await _handler.Handle(command);

        result.StatusCode.Should().NotBe(StatusCode.InternalServerError);
        result.Description.Should().Be("Create histogram.");
    }
}