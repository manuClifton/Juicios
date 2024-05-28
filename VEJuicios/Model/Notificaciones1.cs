using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace VEJuicios.Model
{
    public partial class Notificaciones1
    {
        public Guid Id { get; set; }
        public int AfipSistemaId { get; set; }
        public string Cuit { get; set; }
        public string Asunto { get; set; }
        public string Ref1 { get; set; }
        public string Ref2 { get; set; }
        public long? CommId { get; set; }
        public string Metadata { get; set; }
        public bool EnProceso { get; set; }
        public bool Enviado { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public bool Leida { get; set; }
        public DateTime? FechaNotificacion { get; set; }
        public int IntentosValidarComunicacion { get; set; }
        public bool AfipError { get; set; }
        public string AfipObservacion { get; set; }
    }
}
