using Microsoft.AspNetCore.Http;

namespace VEJuicios.Model
{
    public sealed class NotificacionArchivoAdjuntoModel
    {
        // Propiedad para el archivo adjunto
        public IFormFile Archivo { get; set; }

        // Propiedad para el parámetro notificacionId
        public int NotificacionId { get; set; }

        // Propiedad para el parámetro userId
        public int UserId { get; set; }
        public bool Cedula { get; set; }
        public string Descripcion { get; set; }
        public string MetaData { get; set; }

    }
}
