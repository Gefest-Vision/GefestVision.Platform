﻿namespace GefestVision.Core.Contracts;

public class ProvisionedDeviceInfo
{
    public DeviceInfo DeviceInfo { get; set; }
    public TwinInformation InitialTwin { get; set; }
}
