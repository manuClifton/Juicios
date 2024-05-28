using VEJuicios.Domain.Model.JuiciosNotificaciones;

namespace VEJuicios.Application.UseCases.GenerarNotificacion
{
    public sealed class GenerarNotificacionPDFOutput : Output
    {
        public GenerarNotificacionPDFOutput(byte[] datos, int archivoId, string fileName)
        {
            Datos = datos;
            ArchivoId = archivoId;
            FileName = fileName;
        }

        public byte[] Datos { get; }
        public int ArchivoId { get; }
        public string FileName { get; }
    }
}
