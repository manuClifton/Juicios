using VEJuicios.Domain.Model.JuiciosNotificaciones;

namespace VEJuicios.Application.UseCases.GenerarConstanciaNotificacion
{
    public sealed class GenerarConstanciaNotificacionPDFOutput : Output
    {
        public GenerarConstanciaNotificacionPDFOutput(byte[] datos, int archivoId, string fileName)
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
