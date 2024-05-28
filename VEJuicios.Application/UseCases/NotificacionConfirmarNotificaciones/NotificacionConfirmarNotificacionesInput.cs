using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VEJuicios.Application.UseCases.NotificacionConfirmarNotificaciones
{
    public sealed class NotificacionConfirmarNotificacionesInput
    {
        public int[] NotificacionesId { get; }
        public int UserId { get; }
        public NotificacionConfirmarNotificacionesInput(int[] notificacionesId, int userId)
        {
            NotificacionesId = notificacionesId;
            UserId = userId;
        }
    }
}
