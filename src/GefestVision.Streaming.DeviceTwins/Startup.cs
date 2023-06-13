using GefestVision.Core.Clients;
using GefestVision.Streaming.DeviceTwins;
using GefestVision.Streaming.DeviceTwins.EventProcessing;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]

namespace GefestVision.Streaming.DeviceTwins;

public class Startup : FunctionsStartup
{
    public static ServiceProvider Services;

    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddHttpClient();
        builder.Services.AddSingleton<DeviceRegistryClient>();
        builder.Services.AddTransient<TwinChangedNotificationProcessor>();
        builder.Services.AddTransient<EventProcessor>();

        Services = builder.Services.BuildServiceProvider();
    }
}
