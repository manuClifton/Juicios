using VEJuicios.Application.UseCases.GenerarNotificacion;
using VEJuicios.Model;

namespace VEJuicios.Presenters
{
    public sealed class GenerarNotificacionPresenter : IGenerarNotificacionPDFOutputPort
    {
        public GenerarArchivoViewModel ViewModel { get; set; } = new GenerarArchivoViewModel();

        public void NotFound(string message)
        {
        }

        public void WriteError(string message)
        {
        }

        public void Standard(GenerarNotificacionPDFOutput output)
        {
            ViewModel.File = output.Datos;
            ViewModel.ArchivoId = output.ArchivoId;
            ViewModel.FileName = output.FileName;
        }
    }
}
