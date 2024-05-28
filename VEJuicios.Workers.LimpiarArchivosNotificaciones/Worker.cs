using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

using VEJuicios.Workers.Common;
using VEJuicios.Workers.Common.Configurations;
using VEJuicios.Domain.Model.Notificaciones;

namespace VEJuicios.Workers.LimpiarArvhivosNotificaciones
{
    public class WorkerLimpiarArvhivosNotificaciones : WorkerBase<WorkerLimpiarArvhivosNotificaciones>
    {
        private readonly ILogger<WorkerLimpiarArvhivosNotificaciones> _logger;
        public readonly IConfiguration _config;
        public INotificacionStoreRepository _notificacionStoreRepository;

        public WorkerLimpiarArvhivosNotificaciones(ILogger<WorkerLimpiarArvhivosNotificaciones> logger,
                        IOptions<WorkerMonOptions> workerMonOptions,
                        IConfiguration config,
                        INotificacionStoreRepository notificacionStoreRepository
                     ) : base(logger, workerMonOptions)
        {
            _logger = logger;
            _config = config;
            _notificacionStoreRepository = notificacionStoreRepository;
            
            Exec = true;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            StartAt = DateTime.Now;

            while (!stoppingToken.IsCancellationRequested)
            {
                if (Exec && StartAt < DateTime.Now)
                {
                    _logger.LogInformation("Worker running at: {0} *** running!", DateTimeOffset.Now);

                    await _notificacionStoreRepository.BorrarArchivos();
                }

                await Waiting(stoppingToken);
            }
        }

    }
}