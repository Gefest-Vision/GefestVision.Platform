using GefestVision.Core.Clients;
using GefestVision.Streaming.DeviceTelemetry;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]

namespace GefestVision.Streaming.DeviceTelemetry;

public class Startup : FunctionsStartup
{
    public static ServiceProvider Services;

    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddHttpClient();
        builder.Services.AddSingleton<DeviceRegistryClient>();

        Services = builder.Services.BuildServiceProvider();
    }
}
