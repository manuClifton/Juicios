using VEJuicios.Application.UseCases.NotificacionDescargarAdjuntoEnviado;
using VEJuicios.Model;

namespace VEJuicios.Presenters
{
    public sealed class NotificacionDescargarAdjuntoEnviadoPresenter : INotificacionDescargarAdjuntoEnviadoOutputPort
    {
        public GenerarArchivoViewModel ViewModel { get; set; } = new GenerarArchivoViewModel();
        public void NotFound(string message)
        {
            throw new System.NotImplementedException();
        }

        public void Standard(NotificacionDescargarAdjuntoEnviadoOutput output)
        {
            ViewModel.File = output.Datos;
            ViewModel.FileName = output.FileName;
        }

        public void WriteError(string message)
        {
            throw new System.NotImplementedException();
        }
    }
}
