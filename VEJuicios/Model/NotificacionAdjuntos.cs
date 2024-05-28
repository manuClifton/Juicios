using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace VEJuicios.Model
{
    public partial class NotificacionAdjuntos
    {
        public Guid NotificacionId { get; set; }
        public Guid AdjuntoId { get; set; }
        public DateTime? FechaCreacion { get; set; }
    }
}
