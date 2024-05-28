using VEJuicios.Application.UseCases.GenerarNotificacion;
using VEJuicios.Application.UseCases.GenerarNotificacionPDFEmbargoCuentaSOJ;
using VEJuicios.Model;

namespace VEJuicios.Presenters
{
    public sealed class GenerarPDFEmbargoCuentaSOJPresenter : IGenerarPDFEmbargoCuentaSOJOutputPort
    {
        public GenerarArchivoViewModel ViewModel { get; set; } = new GenerarArchivoViewModel();
        public void NotFound(string message)
        {
            throw new System.NotImplementedException();
        }

        public void WriteError(string message)
        {
            throw new System.NotImplementedException();
        }

        public void Standard(GenerarPDFEmbargoCuentaSOJOutPut output)
        {
            ViewModel.File = output.Datos;
            ViewModel.ArchivoId = output.ArchivoId;
            ViewModel.FileName = output.FileName;
        }
    }
}
