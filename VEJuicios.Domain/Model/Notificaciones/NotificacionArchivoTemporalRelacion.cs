using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VEJuicios.Domain.Model.Notificaciones
{
    public partial class NotificacionArchivoTemporalRelacion
    {
        [Key]
        public int ArchivoId { get; set; }
        public int NotificacionId { get; set; }
        public bool Cedula { get; set; }
        public NotificacionArchivoTemporalRelacion(int archivoId,int notificacionId, bool cedula)
        {
            this.ArchivoId = archivoId;
            this.NotificacionId = notificacionId;
            this.Cedula = cedula;
        }
    }
}
