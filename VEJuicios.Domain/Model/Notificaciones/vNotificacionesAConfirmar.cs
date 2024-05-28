using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VEJuicios.Domain.Model.Notificaciones
{
    public class vNotificacionesAConfirmar
    {
        [Key]
        public int ArchivoTemporalRelacionadoArchivoId { get; set; }
        public int NotificacionId { get; set; }
        public string TipoNotificacionCodigo { get; set; }
        public bool ArchivoTemporalRelacionadoCedula { get; set; }
        public vNotificacionesAConfirmar(int notificacionId, string tipoNotificacionCodigo, bool archivoTemporalRelacionadoCedula)
        {
            this.NotificacionId = notificacionId;
            this.TipoNotificacionCodigo = tipoNotificacionCodigo;
            this.ArchivoTemporalRelacionadoCedula = archivoTemporalRelacionadoCedula;
        }
    }
}
