using Microsoft.Extensions.Options;
using VEJuicios.Domain;
using VEJuicios.Domain.Model.Notificaciones;

namespace VEJuicios.Workers
{
    public class WorkerLimpiarArchivosNotificaciones : BackgroundService
    {
        private readonly ILogger<WorkerLimpiarArchivosNotificaciones> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly WorkerConfig _workerSettings;
        public WorkerLimpiarArchivosNotificaciones(ILogger<WorkerLimpiarArchivosNotificaciones> logger, IOptions<WorkerSettings> workerSettings, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _workerSettings = workerSettings.Value.WorkerLimpiarArchivosNotificaciones;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                using (var scope = _serviceProvider.CreateScope())
                {
                    var scopedService = scope.ServiceProvider.GetRequiredService<INotificacionStorePRepository>();
                    await scopedService.BorrarArchivos();
                };

                await Task.Delay(_workerSettings.RestartInMilliseconds, stoppingToken);

            }
        }
    }
}
