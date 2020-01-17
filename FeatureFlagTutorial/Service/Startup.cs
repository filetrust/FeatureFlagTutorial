using System;
using System.IO;
using Glasswall.FileTrust.RepoName.Initialisation.Dependencies;
using Glasswall.FileTrust.RepoName.Service.BackgroundServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prometheus;
using Swashbuckle.AspNetCore.Swagger;

namespace Glasswall.FileTrust.RepoName.Service
{
    public class Startup
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            _hostingEnvironment = hostingEnvironment ?? throw new ArgumentNullException(nameof(hostingEnvironment));
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            // 
            // There is a link for the above line https://codingblast.com/using-web-api-asp-net-core-without-mvc-specific-stuff/
            // which says makking a call to services.AddMvc() adds alot of MVC specific stuff which isn't required
            // for a pure REST Api.  Therefore,
            //
            // Removed the following packages: Microsoft.AspNetCore.App, Microsoft.AspNetCore.RazorDesign
            // Installed the following packages: /Microsoft.AspNetCore.Mvc.Core, Microsoft.AspNetCore.Mvc.Cors, Microsoft.AspNetCore.Mvc.Formatters.Json
            // And disregard app.UseHsts() & app.UseHttpsRedirection() in the Configure() method that follows later.
            var builder = services.AddMvcCore();

            builder
                .AddApiExplorer()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFormatterMappings()
                .AddJsonFormatters()
                .AddCors();

            var configurationRoot = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            services
                .AddApiVersioning()
                .AddGlasswallServices(configurationRoot)
                .AddHostedService<HostedService>();

            EnableSwagger(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }

            app.UseMetricServer();
            app.UseHttpMetrics();

            //app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseMvc();
        }

        private static void EnableSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Glasswall FileTrust RepoName", Version = "v1.0" });

                var xmlFile = "Service.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }
    }
}
