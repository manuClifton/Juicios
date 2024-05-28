using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VEJuicios.Application.UseCases.NotificacionAlta
{
    public sealed class NotificacionAltaOutput : Output
    {
        public int NotificacionId { get; }
        public NotificacionAltaOutput(int notificacionId)
        {
            NotificacionId = notificacionId;
        }
    }
}
