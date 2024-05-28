using VEJuicios.Application.UseCases.NotificacionArchivosAdjuntos;
using VEJuicios.Model;

namespace VEJuicios.Presenters
{
    public sealed class NotificacionAñadirArchivoPresenter : INotificacionAñadirArchivoPresenterOutputPort
    {
        public AñadirArchivoViewModel ViewModel { get; set; } = new AñadirArchivoViewModel();

        public void NotFound(string message)
        {
        }

        public void WriteError(string message)
        {
        }

        public void Standard(NotificacionAñadirArchivoOutput output)
        {
            ViewModel.ArchivoId = output.ArchivoId;
        }
    }
}
