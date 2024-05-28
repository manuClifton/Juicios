using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace VEJuicios.Model
{
    public partial class Notificaciones
    {
        public string Periodo { get; set; }
        public string Cuit { get; set; }
        public long CommId { get; set; }
        public string Mensaje { get; set; }
        public byte[] Nomina { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public bool? Anulada { get; set; }
        public int? NumeroLiq { get; set; }
        public string Tipo { get; set; }
        public int? LiqId { get; set; }
    }
}
