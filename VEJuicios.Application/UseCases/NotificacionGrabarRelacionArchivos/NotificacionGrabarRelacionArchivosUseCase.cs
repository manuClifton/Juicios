using VEJuicios.Domain.Model.Notificaciones;
using VEJuicios.Domain.Notificaciones;

namespace VEJuicios.Application.UseCases.NotificacionGrabarRelacionArchivos
{
    public sealed class NotificacionGrabarRelacionArchivosUseCase : INotificacionGrabarRelacionArchivosUseCase
    {
        private readonly INotificacionArchivoTemporalRepository _NotificacionArchivoTemporalRepository;
        private readonly INotificacionArchivoTemporalRelacionRepository _NotificacionArchivoTemporalRelacionRepository;
        public NotificacionGrabarRelacionArchivosUseCase(
            INotificacionArchivoTemporalRepository notificacionArchivoTemporalRepository,
            INotificacionArchivoTemporalRelacionRepository notificacionArchivoTemporalRelacionRepository)
        {
            _NotificacionArchivoTemporalRepository = notificacionArchivoTemporalRepository;
            _NotificacionArchivoTemporalRelacionRepository = notificacionArchivoTemporalRelacionRepository;
        }
        public Task Execute(NotificacionGrabarRelacionArchivosInput input)
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
                        _NotificacionArchivoTemporalRelacionRepository.AddRelacion(relation);
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
