using VEJuicios.Application.UseCases.NotificacionConfirmarNotificaciones;

namespace VEJuicios.Presenters
{
    public sealed class NotificacionConfirmarNotificacionesPresenter : INotificacionConfirmarNotificacionesOutputPort
    {
        public string MensajeDeError { get; set; }

        public void NotFound(string message)
        {
        }

        public void WriteError(string message)
        {
        }

        public void Standard(string mensajeDeError)
        {
            MensajeDeError = mensajeDeError;
        }
    }
}
