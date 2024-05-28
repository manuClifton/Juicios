using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VEJuicios.Domain.Model.JuiciosNotificaciones;

namespace VEJuicios.Application.UseCases.NotificacionVerificarMegas
{
    public sealed class NotificacionVerificarMegasInput
    {
        public int[] GuardarArchivosID { get; set; }
        public int[] EliminarArchivosID { get; set; }
        public int NotificacionID { get; set; }
        public int CedulaID { get; set; }
        public NotificacionVerificarMegasInput(int[] guardarArchivosID, int[] eliminarArchivosID, int notificacionID, int cedulaID)
        {
            this.GuardarArchivosID = guardarArchivosID;
            this.EliminarArchivosID = eliminarArchivosID;
            this.NotificacionID = notificacionID;
            this.CedulaID = cedulaID;
        }
    }
}
