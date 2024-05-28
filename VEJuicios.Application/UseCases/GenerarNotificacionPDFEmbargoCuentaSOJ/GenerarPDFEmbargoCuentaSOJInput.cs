using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VEJuicios.Application.UseCases.GenerarNotificacionPDFEmbargoCuentaSOJ
{
    public sealed class GenerarPDFEmbargoCuentaSOJInput
    {
        public DateTime FechaAsociada { get; }
        public DateTime popUpFecha { get; }
        public string CopiasSiNo { get; }
        public int UserId { get; }
        public int NotificacionId { get; }

        public GenerarPDFEmbargoCuentaSOJInput(int notificacionId, string fechaAsociada, string popUpFecha, string CopiasSiNo, int userId)
        {
            this.NotificacionId = notificacionId;
            try
            {
                this.FechaAsociada = DateTime.Parse(fechaAsociada);
            }
            catch (Exception)
            {
                throw new Exception("Fecha inválida.");
            }
            try
            {
                this.popUpFecha = DateTime.Parse(popUpFecha);
            }
            catch (Exception)
            {
                throw new Exception("Fecha inválida.");
            }
            this.CopiasSiNo = CopiasSiNo;
            this.UserId = userId;
        }
    }
}
