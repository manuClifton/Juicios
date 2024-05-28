using VEJuicios.Domain.Model.Notificaciones;

namespace VEJuicios.Domain.Notificaciones
{
    public interface IVNotificacionArchivoEnvioWorkerRepository
    {
        Task<List<VNotificacionArchivoEnvioWorker>> GetAllNotificacionArchivoEnvioWorkerAsync();
        Task<List<VNotificacionArchivoEnvioWorker>> GetNotificacionArchivoEnvioWorkerByNotificacionIdAsync(int id);
    }
}
