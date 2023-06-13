using GefestVision.Actors.Runtime.Device.MessageProcessing.Interfaces;

namespace GefestVision.Actors.Runtime.Device.MessageProcessing;

public class TelemetryMessageProcessor : IMessageProcessor
{
    private readonly ILogger _logger;

    public TelemetryMessageProcessor(ILogger logger)
    {
        _logger = logger;
    }

    public Task ProcessAsync(string rawMessage)
    {
        _logger.LogInformation("Telemetry message received");
        return Task.CompletedTask;
    }
}
