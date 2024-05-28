using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

using VEJuicios.Workers.Common;
using VEJuicios.Workers.Common.Configurations;
using VEJuicios.Application.WorkerUseCases.EnvioAfip;

namespace VEJuicios.Workers.RecepcionAfip
{
    public class WorkerRecepcionAfip : WorkerBase<WorkerRecepcionAfip>
    {
        private readonly ILogger<WorkerRecepcionAfip> _logger;
        public readonly IConfiguration _config;
        public INotificacionRecepcionAfipWorkerUseCase _notificacionRecepcionAfipWorkerUseCase;

        public WorkerRecepcionAfip(ILogger<WorkerRecepcionAfip> logger,
                        IOptions<WorkerMonOptions> workerMonOptions,
                        IConfiguration config,
                        INotificacionRecepcionAfipWorkerUseCase notificacionRecepcionAfipWorkerUseCase
                     ) : base(logger, workerMonOptions)
        {
            _logger = logger;
            _config = config;
            _notificacionRecepcionAfipWorkerUseCase = notificacionRecepcionAfipWorkerUseCase;
            
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

                    await _notificacionRecepcionAfipWorkerUseCase.Execute();
                }

                await Waiting(stoppingToken);
            }
        }

    }
}