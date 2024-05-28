using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace VEJuicios.Model
{
    public partial class Adjuntos
    {
        public Guid Id { get; set; }
        public int? AfipSistemaId { get; set; }
        public string Metadata { get; set; }
        public byte[] Content { get; set; }
        public string NombreArchivo { get; set; }
    }
}
