using Microsoft.Extensions.Options;
using VEJuicios.Application.WorkerUseCases.EnvioAfip;
using VEJuicios.Domain;

namespace VEJuicios.Workers
{
    public class WorkerRecepcionAfip : BackgroundService
    {
        private readonly ILogger<WorkerRecepcionAfip> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly WorkerConfig _workerSettings;

        public WorkerRecepcionAfip(ILogger<WorkerRecepcionAfip> logger, IOptions<WorkerSettings> workerSettings, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _workerSettings = workerSettings.Value.WorkerRecepcionAfip;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                using (var scope = _serviceProvider.CreateScope())
                {
                    var scopedService = scope.ServiceProvider.GetRequiredService<INotificacionRecepcionAfipWorkerUseCase>();
                    bool activar = true;

                    if (activar)
                    {
                        await scopedService.Execute();
                    }
                }
                await Task.Delay(_workerSettings.RestartInMilliseconds, stoppingToken);

            }
        }
    }
}