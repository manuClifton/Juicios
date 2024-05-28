using MediatR;
using VEJuicios.Domain.Model.JuiciosNotificaciones;
using VEJuicios.Domain.Model.Notificaciones;

namespace VEJuicios.Domain.Notificaciones
{
    public interface INotificacionRepository
    {
        Task<List<VistaNotificacion>> GetAllWaitingAfipResponse();
        void ConfirmarNotificacion(int notificacion, int userId);
        List<int> GetNumerosInternosNotificaciones(List<int> notificacionesId);
    }
}
