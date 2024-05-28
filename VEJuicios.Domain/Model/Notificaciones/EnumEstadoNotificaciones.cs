using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VEJuicios.Domain
{
    public enum EnumEstadoNotificaciones
    {
        Borrador = 1,
        PendienteDeEnvio = 2,
        EnviadoWorker = 3,
        EnviadoAfip = 4,
        Recibido = 5, 
        PublicadoVE = 6,
        Notificado = 7,
        CanceladoAfip = 99

    }
}
