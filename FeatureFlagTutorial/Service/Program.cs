using Glasswall.Common.Instrumentation;
using Glasswall.FileTrust.RepoName.Business.Instrumentation;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Prometheus;

namespace Glasswall.FileTrust.RepoName.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SetupKestrel();
            CreateWebHostBuilder(args).Build().Run();
        }

        private static void SetupKestrel()
        {
            const int metricsPort = 9089;
            var metricServer = new KestrelMetricServer(metricsPort);
            metricServer.Start();
            InstrumentationInitialiser.Initialise(typeof(ApiInstrumentation));
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddDebug();
                })
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var settings = config.Build();
                    config.AddAzureAppConfiguration(options =>
                    {
                        options.Connect(settings["FeatureFlagTutorial"])
                            .UseFeatureFlags();
                    });
                });
    }
}
