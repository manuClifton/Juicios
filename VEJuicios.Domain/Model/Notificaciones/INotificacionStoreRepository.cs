using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VEJuicios.Domain.Model.Notificaciones
{
    public interface INotificacionStoreRepository
    {
        Task EnvioAfip(int notificacionId, Guid idAfip = new Guid(), string error = "");
        Task BorrarArchivos();
        Task RecepcionAfip(int notificacionId, EnumEstadoNotificaciones estadoId, DateTime? fechaEnvio = null, DateTime? fechaRecepcionAfip = null, DateTime? fechaNotificacion = null, DateTime? fechaCancelacion = null);
        Task GenerarLog(string log);
    }
}
