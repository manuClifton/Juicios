using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VEJuicios.Domain.Model.Notificaciones
{
    public class VNotificacionArchivoEnvioWorker
    {
        public int NotificacionId { get; set; }
        public int ArchivoId { get; set; }
        public bool Cedula { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
        public string MetaData { get; set; }
        public byte[] Data { get; set; }
        public bool GeneradoAutomatico { get; set; }
        public int UsuarioInsertId { get; set; }
        public string TipoNotificacionDescripcion { get; set; }
        public DateTime FechaInsert { get; set; }
    }
}
