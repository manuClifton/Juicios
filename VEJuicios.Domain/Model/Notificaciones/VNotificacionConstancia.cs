using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VEJuicios.Domain.Model.Notificaciones
{
    public class VNotificacionConstancia
    {
        [Key]
        public int NotificacionId { get; set; }
        public DateTime? FechaRecepcionAfip { get; set; }
        public DateTime? FechaNotificacion { get; set; }
        public string Cuit { get; set; }
        public string PersonaNombre { get; set; }
        public string PersonaDireccion { get; set; }
        public string Carpeta { get; set; }
        public string DescripcionMotivo { get; set; }
        public string? Juzgado { get; set; }
        public string? JuzgadoDireccionCompleta { get; set; }
        public string? Secretaria { get; set; }
        public string? Expediente { get; set; }
    }
}
