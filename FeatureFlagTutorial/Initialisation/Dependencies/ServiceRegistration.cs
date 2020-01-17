using System;
using System.Diagnostics.CodeAnalysis;
using Glasswall.Kernel.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using Glasswall.Providers.Logging.Console.CustomConsoleLogger;
using Microsoft.Extensions.Configuration;

namespace Glasswall.FileTrust.RepoName.Initialisation.Dependencies
{
    [ExcludeFromCodeCoverage]
    public static class ServiceRegistration
    {
        public static IServiceCollection AddGlasswallServices(this IServiceCollection services, IConfigurationRoot configurationRoot)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddServiceLogging(configurationRoot);
            services.AddSingleton(typeof(IGWLogger<>), typeof(Providers.Logging.Microsoft.Logger<>));
            services.TryAddTransient<ILoggerProvider, DebugLoggerProvider>();

            AddBusinessLayer(services);

            return services;
        }

        private static void AddBusinessLayer(IServiceCollection services)
        {
        }
    }
}