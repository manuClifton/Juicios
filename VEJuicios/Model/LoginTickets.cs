using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace VEJuicios.Model
{
    public partial class LoginTickets
    {
        public long Id { get; set; }
        public long UniqueId { get; set; }
        public DateTime? ExpirationTime { get; set; }
        public DateTime? GenerationTime { get; set; }
        public string Service { get; set; }
        public string Sign { get; set; }
        public string Token { get; set; }
        public string Response { get; set; }
    }
}
