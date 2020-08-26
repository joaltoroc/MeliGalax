namespace MeliGalax.Service
{
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class TimedHostedService : IHostedService, IDisposable
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<TimedHostedService> Logger;

        /// <summary>
        /// The domain datos
        /// </summary>
        private readonly Domain.DomainDatos.IDomainDatos DomainDatos;

        /// <summary>
        /// The timer
        /// </summary>
        private Timer Timer;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimedHostedService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="domainDatos">The domain datos.</param>
        public TimedHostedService(ILogger<TimedHostedService> logger,
                                  Domain.DomainDatos.IDomainDatos domainDatos)
        {
            Logger = logger;
            DomainDatos = domainDatos;
        }

        /// <summary>
        /// Triggered when the application host is ready to start the service.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the start process has been aborted.</param>
        /// <returns></returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Logger.LogInformation("Service is starting.");

            Timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromHours(24));

            return Task.CompletedTask;
        }

        /// <summary>
        /// Does the work.
        /// </summary>
        /// <param name="state">The state.</param>
        private void DoWork(object state)
        {
            Logger.LogInformation("Service is running.");

            Task.Run(async () =>
            {
                await DomainDatos.Handle();
            });
        }

        /// <summary>
        /// Triggered when the application host is performing a graceful shutdown.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the shutdown process should no longer be graceful.</param>
        /// <returns></returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            Logger.LogInformation("Service is stopping.");

            Timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Timer?.Dispose();
        }
    }
}