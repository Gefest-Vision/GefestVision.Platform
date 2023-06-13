namespace GefestVision.Actors.Runtime.Device.MessageProcessing.Interfaces;

public interface IMessageProcessor
{
    Task ProcessAsync(string rawMessage);
}
