using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VEJuicios.Application.UseCases.NotificacionAlta
{
    public sealed class NotificacionAltaInput
    {
        public int CasoId { get; set; }
        public int ParteId { get; set; }
        public int TipoNotificacionId { get; set; }
        public int TipoNotificacionDetalleId { get; set; }
        public int LiquidacionId { get; set; }
        public int OficioBCRAId { get; set; }
        public int UsuarioId { get; set; }
    }
}
