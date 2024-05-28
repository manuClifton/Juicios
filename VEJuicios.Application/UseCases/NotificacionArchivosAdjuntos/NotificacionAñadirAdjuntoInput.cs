using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VEJuicios.Domain.Model.JuiciosNotificaciones;
using VEJuicios.Domain.Model.Notificaciones;

namespace VEJuicios.Application.UseCases.NotificacionArchivosAdjuntos
{
    public sealed class NotificacionAñadirAdjuntoInput
    {
        public int UserId { get; }
        public NotificacionArchivoTemporal Adjunto { get; }

        public NotificacionAñadirAdjuntoInput(int userId, NotificacionArchivoTemporal adjunto)
        {
            this.UserId = userId;
            this.Adjunto = adjunto;
        }


    }
}
