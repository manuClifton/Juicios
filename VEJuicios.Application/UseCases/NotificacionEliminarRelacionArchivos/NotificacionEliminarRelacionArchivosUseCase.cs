
using VEJuicios.Domain.Model.Notificaciones;
using VEJuicios.Domain.Notificaciones;

namespace VEJuicios.Application.UseCases.NotificacionEliminarRelacionArchivos
{
    public sealed class NotificacionEliminarRelacionArchivosUseCase : INotificacionEliminarRelacionArchivosUseCase
    {
        private readonly INotificacionArchivoTemporalRelacionRepository _NotificacionArchivoTemporalRelacionRepository;
        public NotificacionEliminarRelacionArchivosUseCase(
            INotificacionArchivoTemporalRelacionRepository notificacionArchivoTemporalRelacionRepository)
        {
            _NotificacionArchivoTemporalRelacionRepository = notificacionArchivoTemporalRelacionRepository;
        }

        public Task Execute(NotificacionEliminarRelacionArchivosInput input)
        {
            try
            {
                for (int i = 0; i < input.ArchivosId.Length; i++)
                {
                    if (input.ArchivosId[i] > 0)
                    {

                        bool cedula = false;

                        if (input.ArchivosId[i] == input.CedulaId)
                        {
                            cedula = true;
                        }
                        var relation = new NotificacionArchivoTemporalRelacion(input.ArchivosId[i], input.NotificacionId, cedula);
                        _NotificacionArchivoTemporalRelacionRepository.DeleteRelacion(relation);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }

            return Task.CompletedTask;
        }
    }
}
