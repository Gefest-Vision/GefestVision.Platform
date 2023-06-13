using Dapr.Actors;
using GefestVision.Actors.Device.Abstractions.Contracts;

namespace GefestVision.Actors.Device.Abstractions;

public interface IDeviceActor : IActor
{
    Task ProvisionAsync(DeviceInfo info);
    Task SetInfoAsync(DeviceInfo info);
    Task<DeviceInfo> GetInfoAsync();
    Task ReceiveMessageAsync(MessageTypes type, string rawMessage);
    Task SetReportedPropertyAsync(Dictionary<string, string> reportedProperties);
}
