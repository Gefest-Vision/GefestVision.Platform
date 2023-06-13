namespace GefestVision.API.Management.Exceptions;

/// <inheritdoc />
public class UnknownDeviceException : Exception
{
    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="imei">Imei of the device</param>
    public UnknownDeviceException(string imei)
    {
        Imei = imei;
    }

    public UnknownDeviceException()
    {
    }

    public UnknownDeviceException(string message, Exception innerException) : base(message, innerException)
    {
    }

    /// <summary>
    ///     Imei of the device
    /// </summary>
    public string Imei { get; }
}
