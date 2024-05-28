using VEJuicios.Application.UseCases.NotificacionVerificarMegas;
using VEJuicios.Model;

namespace VEJuicios.Presenters
{
    public sealed class VNotificacionVerificarMegasPresenter : INotificacionVerigficarMegasPresenterOutputPort
    {
        public GenerarArchivoViewModel ViewModel { get; set; } = new GenerarArchivoViewModel();
        public bool res;
        public void NotFound(string message)
        {
            throw new System.NotImplementedException();
        }

        public void Standard(bool output)
        {
            res = output;
        }

        public void WriteError(string message)
        {
            throw new System.NotImplementedException();
        }
    }
}
