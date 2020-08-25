namespace MeliGalax.Service
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using System;
    using System.IO;
    using System.Threading.Tasks;

    public static class Program
    {
        public static async Task Main()
        {
            var hostBuilder = new HostBuilder()
                .ConfigureAppConfiguration((hostContext, configBuilder) =>
                {
                    configBuilder.SetBasePath(Directory.GetCurrentDirectory());
                    configBuilder.AddJsonFile("appsettings.json", optional: true);
                    configBuilder.AddEnvironmentVariables();
                    configBuilder.AddAzureKeyVault(
                        $"https://{Environment.GetEnvironmentVariable(Models.Constants.Environments.KeyUrl)}.vault.azure.net/",
                        Environment.GetEnvironmentVariable(Models.Constants.Environments.KeyId),
                        Environment.GetEnvironmentVariable(Models.Constants.Environments.KeySecret));
                })
                .ConfigureLogging((hostContext, configLogging) =>
                {
                    configLogging.AddConfiguration(hostContext.Configuration.GetSection("Logging"));
                    configLogging.AddConsole();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddData(hostContext.Configuration);
                    services.AddDomain();
                    
                    services.AddScoped<IHostedService, TimedHostedService>();
                });

            await hostBuilder.RunConsoleAsync();
        }
    }
}
