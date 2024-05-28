using VEJuicios.Domain.Model.Notificaciones;

namespace VEJuicios.Domain.Notificaciones
{
    public interface INotificacionArchivoTemporalRepository : IRepository<NotificacionArchivoTemporal>
    {
        int AddArchivo(NotificacionArchivoTemporal entity);
        void DeleteArchivo(int id);
        NotificacionArchivoTemporal GetById(int id);
        List<NotificacionArchivoTemporal> GetByArrayId(int[] archivosId);
    }
}
