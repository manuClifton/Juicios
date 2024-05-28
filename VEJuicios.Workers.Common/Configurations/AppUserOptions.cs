using System.Net;

namespace VEJuicios.Services
{
    public class AppUserOptions
    {
        public NetworkCredential Credential { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string Domain { get; set; }
    }
}
