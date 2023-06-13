using GefestVision.Streaming.DeviceTwins.Contracts;

namespace GefestVision.Streaming.DeviceTwins.EventProcessing.Interfaces;

public interface IEventProcessor
{
    Task ProcessAsync(NotificationMetadata notificationMetadata, string rawEvent);
}
