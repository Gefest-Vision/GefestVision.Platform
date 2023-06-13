using GefestVision.Streaming.DeviceTwins.Contracts;
using GefestVision.Streaming.DeviceTwins.EventProcessing.Interfaces;
using GuardNet;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GefestVision.Streaming.DeviceTwins.EventProcessing;

public class EventProcessor
{
    private readonly ILogger<EventProcessor> _logger;
    private readonly IServiceProvider _serviceProvider;

    public EventProcessor(IServiceProvider serviceProvider, ILogger<EventProcessor> logger)
    {
        Guard.NotNull(serviceProvider, nameof(serviceProvider));
        Guard.NotNull(logger, nameof(logger));

        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task ProcessAsync(NotificationMessageSchemas messageSchema, NotificationMetadata notificationMetadata,
        string rawMessage)
    {
        IEventProcessor processor;

        switch (messageSchema)
        {
            case NotificationMessageSchemas.TwinChangeNotification:
                processor = _serviceProvider.GetRequiredService<TwinChangedNotificationProcessor>();
                break;
            default:
                _logger.LogWarning("Message schema {MessageSchema} is not supported and being ignored",
                    notificationMetadata.RawMessageSchema);
                return;
        }

        await processor.ProcessAsync(notificationMetadata, rawMessage);
    }
}
