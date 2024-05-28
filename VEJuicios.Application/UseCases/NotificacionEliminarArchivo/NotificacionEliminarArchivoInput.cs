namespace VEJuicios.Application.UseCases.NotificacionEliminarArchivo
{
    public sealed class NotificacionEliminarArchivoInput
    {
        public int[] ArchivosId { get; }
        public NotificacionEliminarArchivoInput(int[] archivosId)
        {
            ArchivosId = archivosId;
        }
    }
}