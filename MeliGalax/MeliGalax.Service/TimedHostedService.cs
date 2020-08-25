namespace MeliGalax.Service
{
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class TimedHostedService : IHostedService, IDisposable
    {
        private readonly ILogger<TimedHostedService> Logger;

        private readonly Domain.DomainDatos.IDomainDatos DomainDatos;

        private Timer Timer;

        public TimedHostedService(ILogger<TimedHostedService> logger,
                                  Domain.DomainDatos.IDomainDatos domainDatos)
        {
            Logger = logger;
            DomainDatos = domainDatos;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Logger.LogInformation("Service is starting.");

            Timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromHours(24));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            Logger.LogInformation("Service is running.");

            Task.Run(async () =>
            {
                await DomainDatos.Handle();
            });
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Logger.LogInformation("Service is stopping.");

            Timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Timer?.Dispose();
        }
    }
}