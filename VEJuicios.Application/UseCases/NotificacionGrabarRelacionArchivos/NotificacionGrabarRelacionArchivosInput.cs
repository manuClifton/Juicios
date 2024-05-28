namespace VEJuicios.Application.UseCases.NotificacionGrabarRelacionArchivos
{
    public sealed class NotificacionGrabarRelacionArchivosInput
    {
        public int[] ArchivosId { get; }
        public int NotificacionId { get; }
        public int CedulaId { get; }
        public NotificacionGrabarRelacionArchivosInput(int[] archivosId, int notificacionId, int cedulaId)
        {
            ArchivosId = archivosId;
            NotificacionId = notificacionId;
            CedulaId = cedulaId;
        }
    }
}
