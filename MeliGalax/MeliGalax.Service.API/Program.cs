using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace MeliGalax.Service.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddAzureKeyVault(
                    $"https://{Environment.GetEnvironmentVariable(Models.Constants.Environments.KeyUrl)}.vault.azure.net/",
                        Environment.GetEnvironmentVariable(Models.Constants.Environments.KeyId),
                        Environment.GetEnvironmentVariable(Models.Constants.Environments.KeySecret));
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
