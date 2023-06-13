using GuardNet;
using Microsoft.Azure.EventHubs;

namespace GefestVision.Streaming.DeviceTwins.Contracts;

public class NotificationMetadata
{
    private NotificationMetadata(string ioTHubName, string deviceId, NotificationMessageSchemas messageSchema,
        string rawMessageSchema, string correlationId)
    {
        Guard.NotNullOrWhitespace(ioTHubName, nameof(ioTHubName));
        Guard.NotNullOrWhitespace(deviceId, nameof(deviceId));

        IoTHubName = ioTHubName;
        DeviceId = deviceId;
        MessageSchema = messageSchema;
        RawMessageSchema = rawMessageSchema;
        CorrelationId = correlationId;
    }

    public string DeviceId { get; }
    public string IoTHubName { get; }
    public string RawMessageSchema { get; }
    public NotificationMessageSchemas MessageSchema { get; }
    public string CorrelationId { get; }

    public static NotificationMetadata Parse(EventData eventData)
    {
        Guard.NotNull(eventData, nameof(eventData));

        var deviceId = eventData.Properties[EventProperties.DeviceId]?.ToString();
        var ioTHubName = eventData.Properties[EventProperties.IoTHubName]?.ToString();
        var rawMessageSchema = eventData.Properties[EventProperties.MessageSchema]?.ToString();
        var correlationId = eventData.SystemProperties[EventSystemProperties.CorrelationId].ToString();

        var messageSchema = NotificationMessageSchemas.Unknown;
        if (Enum.TryParse(rawMessageSchema, true, out NotificationMessageSchemas parsedSchema))
        {
            messageSchema = parsedSchema;
        }

        return new NotificationMetadata(ioTHubName, deviceId, messageSchema, rawMessageSchema, correlationId);
    }
}
