using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VEJuicios.Domain.Model.Notificaciones
{
    public partial class NotificacionAdjuntoEnviado
    {
        public decimal NotificacionId { get; set; }
        public Guid ReferenciaApiId { get; set; }
        public bool Cedula { get; set; }
        public bool GeneradoAutomatico { get; set; }
        public string FileName { get; set; }
        public string TipoNotificacionDescripcion { get; set; }
        public DateTime FechaInsert { get; set; }
        public int UsuarioInsertId { get; set; }


        public NotificacionAdjuntoEnviado(decimal notificacionId, Guid referenciaApiId, bool cedula, bool generadoAutomatico, string fileName, string tipoNotificacionDescripcion, DateTime fechaInsert, int usuarioInsertId)
        {
            this.NotificacionId = notificacionId;
            this.ReferenciaApiId = referenciaApiId;
            this.Cedula = cedula;
            this.GeneradoAutomatico = generadoAutomatico;
            this.FileName = fileName;
            this.TipoNotificacionDescripcion = tipoNotificacionDescripcion;
            this.FechaInsert = fechaInsert;
            this.UsuarioInsertId = usuarioInsertId;
        }

    }
}