using VEJuicios.Domain.Model.Notificaciones;

namespace VEJuicios.Domain.Notificaciones
{
    public interface IVistaNotificacionRepository
    {
        Task<List<VistaNotificacion>> GetAllPendientesEnvio();
    }
}
