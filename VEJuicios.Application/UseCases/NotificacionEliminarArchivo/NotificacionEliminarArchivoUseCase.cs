using VEJuicios.Domain.Notificaciones;

namespace VEJuicios.Application.UseCases.NotificacionEliminarArchivo
{
    public sealed class NotificacionEliminarArchivoUseCase : INotificacionEliminarArchivoUseCase
    {
        private readonly INotificacionArchivoTemporalRepository _NotificacionArchivoTemporalRepository;
        public NotificacionEliminarArchivoUseCase(INotificacionArchivoTemporalRepository notificacionArchivoTemporalRepository)
        {
            this._NotificacionArchivoTemporalRepository = notificacionArchivoTemporalRepository;
        }

        public Task Execute(NotificacionEliminarArchivoInput input)
        {
            try
            {
                foreach(var archivoId in input.ArchivosId)
                    this._NotificacionArchivoTemporalRepository.DeleteArchivo(archivoId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }

            return Task.CompletedTask;
        }
    }
}
