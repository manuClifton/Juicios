using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VEJuicios.Domain.Model.Notificaciones
{
    public partial class NotificacionArchivoTemporal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ArchivoId { get; set; }
        public string FileName { get; set; }
        public bool Cedula { get; set; }
        public string MetaData { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
        public bool GeneradoAutomatico { get; set; }
        public DateTime FechaInsert { get; set; }
        public DateTime FechaUpdate { get; set; }
        public int UsuarioInsertId { get; set; }
        public int UsuarioUpdateId { get; set; }
        public string TipoNotificacionDescripcion { get; set; }
        // Constructor sin parámetros requerido para Entity Framework Core
        public NotificacionArchivoTemporal() { }
        public NotificacionArchivoTemporal(string fileName, bool cedula, string metadata,string contentType, byte[] data, bool generadoAutomatico, DateTime fechaInsert, DateTime fechaUpdate, int usuarioInsertId, int usuarioUpdateId, string tipoNotificacionDescripcion)
        {
            this.FileName = fileName;
            this.Cedula = cedula;
            this.MetaData = metadata;
            this.ContentType = contentType;
            this.Data = data;
            this.GeneradoAutomatico = generadoAutomatico;
            this.FechaInsert = fechaInsert;
            this.FechaUpdate = fechaUpdate;
            this.UsuarioInsertId = usuarioInsertId;
            this.UsuarioUpdateId = usuarioUpdateId;
            this.TipoNotificacionDescripcion = tipoNotificacionDescripcion;
        }

    }
}
