namespace GefestVision.Core.Contracts;

public class TwinProperties
{
    public Dictionary<string, object> Reported { get; set; }
    public Dictionary<string, object> Desired { get; set; }
}
