using Microsoft.AspNetCore.Http;
using VEJuicios.Application.UseCases.GenerarNotificacion;
using VEJuicios.Domain.Model.JuiciosNotificaciones;
using VEJuicios.Domain.Notificaciones;

namespace VEJuicios.Application.UseCases.NotificacionArchivosAdjuntos
{
    public sealed class NotificacionAñadirAdjuntoUseCase : INotificacionAñadirAdjuntoUseCase
    {
        private readonly INotificacionArchivoTemporalRepository _NotificacionArchivoTemporalRepository;
        private readonly INotificacionAñadirArchivoPresenterOutputPort _AñadirArchivoPresenterOutputPort;

        public NotificacionAñadirAdjuntoUseCase(
            INotificacionArchivoTemporalRepository notificacionArchivoTemporalRepository,
            INotificacionAñadirArchivoPresenterOutputPort añadirArchivoPresenterOutputPort
            )
        {
            this._NotificacionArchivoTemporalRepository = notificacionArchivoTemporalRepository;
            this._AñadirArchivoPresenterOutputPort = añadirArchivoPresenterOutputPort;

        }

        public Task Execute(NotificacionAñadirAdjuntoInput input)
        {
            if (input.UserId == 0)
            {
                throw new Exception("El usuario no ha sido enviado");
            }
            else if (input.Adjunto.Data == null)
            {
                throw new Exception("La data no ha sido enviado");
            }
            else if (input.Adjunto.ContentType == "")
            {
                throw new Exception("El ContentType no ha sido enviado");
            }
            else
            {
                try
                {

                    int archivoId = this._NotificacionArchivoTemporalRepository.AddArchivo(input.Adjunto);

                    var output = new NotificacionAñadirArchivoOutput(archivoId);
                    this._AñadirArchivoPresenterOutputPort.Standard(output);

                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }

            return Task.CompletedTask;
        }
    }
}
