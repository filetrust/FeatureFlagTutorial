using System;
using System.Threading;
using System.Threading.Tasks;
using Glasswall.Kernel.Logging;
using Microsoft.Extensions.Hosting;

namespace Glasswall.FileTrust.RepoName.Service.BackgroundServices
{
    public class HostedService : BackgroundService
    {
        private readonly IGWLogger<HostedService> _logger;

        public HostedService(IGWLogger<HostedService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.Log(LogLevel.Information, (int)Common.Logging.EventId.ServiceStarting, "The hosted service is starting...");

            // Background logic goes here.

            _logger.Log(LogLevel.Information, (int)Common.Logging.EventId.ServiceStarted, "The hosted service has started.");

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
    }
}
