using VEJuicios.Application.UseCases.NotificacionAlta;

namespace VEJuicios.Application.UseCases.InfractorAlta
{
    public interface IInfractorAltaOutputPort : IOutputPortStandard<InfractorAltaOutput>,
        IOutputPortError, IOutputPortNotFound
    {
    }
}
