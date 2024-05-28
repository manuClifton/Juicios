using VEJuicios.Domain.Model.Notificaciones;

namespace VEJuicios.Domain.Notificaciones
{
    public interface INotificacionArchivoTemporalRelacionRepository : IRepository<NotificacionArchivoTemporalRelacion>
    {
        int AddRelacion(NotificacionArchivoTemporalRelacion entity);
        void DeleteRelacion(NotificacionArchivoTemporalRelacion entity);
    }
}
