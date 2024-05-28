using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VEJuicios.Application.UseCases.NotificacionGenerarCedulaValidarDatos
{
    public sealed class NotificacionGenerarCedulaValidarDatosInput
    {
        public int NotificacionId { get; set; }
        public NotificacionGenerarCedulaValidarDatosInput(int notificacionId)
        {
            NotificacionId = notificacionId;
        }
    }
}
