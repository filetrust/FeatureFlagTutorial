using System;
using System.Threading;
using System.Threading.Tasks;
using Glasswall.Kernel.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.FeatureManagement;

namespace Glasswall.FileTrust.RepoName.Service.BackgroundServices
{
    public class HostedService : BackgroundService
    {
        private readonly IGWLogger<HostedService> _logger;
        private readonly IFeatureManager _featureManager;

        public HostedService(IGWLogger<HostedService> logger, IFeatureManager featureManager)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _featureManager = featureManager;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.Log(LogLevel.Information, (int)Common.Logging.EventId.ServiceStarting, "The hosted service is starting...");

            // Background logic goes here.

            _logger.Log(LogLevel.Information, (int)Common.Logging.EventId.ServiceStarted, "The hosted service has started.");

            while (true)
            {
                if (await _featureManager.IsEnabledAsync(nameof(MyFeatureFlags.Beta)))
                {
                    _logger.Log(LogLevel.Information, (int)Common.Logging.EventId.ServiceStarted, "My feature is Enabled!");
                }
                else
                {
                    _logger.Log(LogLevel.Information, (int)Common.Logging.EventId.ServiceStarted, "My feature is Disabled!");
                }

                Thread.Sleep(TimeSpan.FromSeconds(5));
            }

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
    }
}
