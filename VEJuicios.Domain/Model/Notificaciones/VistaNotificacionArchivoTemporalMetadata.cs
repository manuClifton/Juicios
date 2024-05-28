using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VEJuicios.Domain.Model.Notificaciones
{
    public partial class VistaNotificacionArchivoTemporalMetadata
    {
        [Key]
        public int ArchivoId { get; set; }
        public string MetaData { get; set; }
        public int NotificacionId { get; set; }
        // Constructor sin parámetros requerido para Entity Framework Core
        public VistaNotificacionArchivoTemporalMetadata() { }
        public VistaNotificacionArchivoTemporalMetadata(int archivoId, string metadata, int notificacionId  )
        {
            this.ArchivoId = archivoId;
            this.MetaData = metadata;
            this.NotificacionId = notificacionId;
        }
    }
}
