using VEJuicios.Application.UseCases.NotificacionGenerarCedulaValidarDatos;

namespace VEJuicios.Presenters
{
    public sealed class NotificacionGenerarCedulaValidarDatosPresenter : INotificacionGenerarCedulaValidarDatosOutputPort
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
