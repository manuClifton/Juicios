namespace VEJuicios.Application.UseCases.GenerarConstanciaNotificacion
{
    public interface IGenerarConstanciaNotificacionPDFOutputPort : IOutputPortStandard<GenerarConstanciaNotificacionPDFOutput>,
        IOutputPortError, IOutputPortNotFound
    {
    }
}
