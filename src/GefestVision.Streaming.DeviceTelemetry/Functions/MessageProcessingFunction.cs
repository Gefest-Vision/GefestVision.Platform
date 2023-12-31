﻿using GefestVision.Core.Clients;
using GefestVision.Core.Contracts;
using GefestVision.Streaming.Runtimes.AzureFunctions.Functions;
using GuardNet;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace GefestVision.Streaming.DeviceTelemetry.Functions;

public class MessageProcessingFunction : AzureEventHubsFunction
{
    private const string DeviceId = "deviceId";
    private const string MessageType = "messageType";
    private readonly DeviceRegistryClient _deviceRegistryClient;

    public MessageProcessingFunction(DeviceRegistryClient deviceRegistryClient)
    {
        Guard.NotNull(deviceRegistryClient, nameof(deviceRegistryClient));

        _deviceRegistryClient = deviceRegistryClient;
    }

    [FunctionName("device-message-processor")]
    public async Task Run(
        [EventHubTrigger("device-messages", Connection = "EventHubs.ConnectionStrings.DeviceMessages")]
        EventData[] events, ILogger logger)
    {
        await ProcessEventsAsync(events);
    }

    protected override async Task ProcessIndividualEventAsync(EventData eventData, string rawEventPayload)
    {
        var deviceId = eventData.Properties[DeviceId]?.ToString();
        var rawMessageType = eventData.Properties[MessageType]?.ToString();

        if (Enum.TryParse(rawMessageType, true, out MessageTypes messageType))
        {
            await _deviceRegistryClient.SendMessageAsync(deviceId, messageType, rawEventPayload);
        }
        else
        {
            throw new Exception($"Unable to process message with type {rawMessageType}");
        }
    }
}
