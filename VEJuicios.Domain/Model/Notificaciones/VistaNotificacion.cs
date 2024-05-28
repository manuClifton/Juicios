using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VEJuicios.Domain.Model.Notificaciones
{
    public class VistaNotificacion
    {
        public int NotificacionId { get; set; }
        public EnumEstadoNotificaciones NotiEstadoId { get; set; }
        public string Cuit { get; set; }
        public EnumEstadoNotificaciones EstadoId { get; set; }
        public Guid? AfipId { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public DateTime? FechaRecepcionAfip { get; set; }
        public DateTime? FechaNotificacion { get; set; }
        public DateTime? FechaCancelacion { get; set; }
    }
}
