

namespace VEJuicios.Application.UseCases.NotificacionEliminarRelacionArchivos
{
    public sealed class NotificacionEliminarRelacionArchivosInput
    {
        public int[] ArchivosId { get; }
        public int NotificacionId { get; }
        public int CedulaId { get; }
        public NotificacionEliminarRelacionArchivosInput(int[] archivosId, int notificacionId, int cedulaId)
        {
            ArchivosId = archivosId;
            NotificacionId = notificacionId;
            CedulaId = cedulaId;
        }
    }
}
