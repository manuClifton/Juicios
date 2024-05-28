using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

using VEJuicios.Workers.Common;
using VEJuicios.Workers.Common.Configurations;
using VEJuicios.Application.WorkerUseCases.EnvioAfip;

namespace VEJuicios.Workers.EnvioAfip
{
    public class WorkerEnvioAfip : WorkerBase<WorkerEnvioAfip>
    {
        private readonly ILogger<WorkerEnvioAfip> _logger;
        public readonly IConfiguration _config;
        public INotificacionEnvioAfipWorkerUseCase _notificacionEnvioAfipWorkerUseCase;

        public WorkerEnvioAfip(ILogger<WorkerEnvioAfip> logger,
                        IOptions<WorkerMonOptions> workerMonOptions,
                        IConfiguration config,
                        INotificacionEnvioAfipWorkerUseCase notificacionEnvioAfipWorkerUseCase
                     ) : base(logger, workerMonOptions)
        {
            _logger = logger;
            _config = config;
            _notificacionEnvioAfipWorkerUseCase = notificacionEnvioAfipWorkerUseCase;
            
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

                    await _notificacionEnvioAfipWorkerUseCase.ExecuteSync();
                }

                await Waiting(stoppingToken);
            }
        }

    }
}