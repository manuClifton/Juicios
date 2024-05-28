namespace VEJuicios.Application.UseCases.GenerarNotificacion
{
    public interface IGenerarNotificacionPDFOutputPort : IOutputPortStandard<GenerarNotificacionPDFOutput>,
        IOutputPortError, IOutputPortNotFound
    {
    }
}
