using Serilog;

namespace GefestVision.API.Management;

public class Program
{
    public static int Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        try
        {
            CreateHostBuilder(args)
                .Build()
                .Run();

            return 0;
        }
        catch (Exception exception)
        {
            Log.Fatal(exception, "Host terminated unexpectedly");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        var configuration = CreateConfiguration(args);
        var webHostBuilder = CreateHostBuilder(args, configuration);

        return webHostBuilder;
    }

    private static IConfiguration CreateConfiguration(string[] args)
    {
        var configuration =
            new ConfigurationBuilder()
                .AddCommandLine(args)
                .AddEnvironmentVariables()
                .Build();

        return configuration;
    }

    private static IHostBuilder CreateHostBuilder(string[] args, IConfiguration configuration)
    {
        var httpEndpointUrl = "http://+:" + configuration["HTTP_PORT"];
        var webHostBuilder =
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(configBuilder => configBuilder.AddConfiguration(configuration))
                .ConfigureSecretStore((_, secretStoreBuilder) =>
                {
                    secretStoreBuilder.AddEnvironmentVariables();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(kestrelServerOptions => kestrelServerOptions.AddServerHeader = false)
                        .UseUrls(httpEndpointUrl)
                        .UseSerilog()
                        .UseStartup<Startup>();
                });

        return webHostBuilder;
    }
}
