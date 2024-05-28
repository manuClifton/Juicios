using VEJuicios.Application.UseCases.NotificacionAlta;

namespace VEJuicios.Presenters
{
    public sealed class NotificacionAltaPresenter : INotificacionAltaPresenterOutputPort
    {
        public int NotificacionId { get; set; }
        public void NotFound(string message)
        {
            throw new System.NotImplementedException();
        }

        public void Standard(NotificacionAltaOutput output)
        {
            NotificacionId = output.NotificacionId;
        }

        public void WriteError(string message)
        {
            throw new System.NotImplementedException();
        }
    }
}
