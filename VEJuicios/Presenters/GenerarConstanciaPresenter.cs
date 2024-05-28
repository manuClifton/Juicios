using VEJuicios.Application.UseCases.GenerarConstanciaNotificacion;
using VEJuicios.Model;

namespace VEJuicios.Presenters
{
    public sealed class GenerarConstanciaPresenter : IGenerarConstanciaNotificacionPDFOutputPort
    {
        public GenerarArchivoViewModel ViewModel { get; set; } = new GenerarArchivoViewModel();

        public void NotFound(string message)
        {
        }

        public void WriteError(string message)
        {
        }

        public void Standard(GenerarConstanciaNotificacionPDFOutput output)
        {
            ViewModel.File = output.Datos;
            ViewModel.ArchivoId = output.ArchivoId;
            ViewModel.FileName = output.FileName;
        }
    }
}
