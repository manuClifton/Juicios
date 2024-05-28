using VEJuicios.Application.UseCases.InfractorAlta;

namespace VEJuicios.Presenters
{
    public sealed class InfractorAltaPresenter : IInfractorAltaOutputPort
    {
        public int InfractorId { get; set; }
        public void NotFound(string message)
        {
            throw new System.NotImplementedException();
        }

        public void Standard(InfractorAltaOutput output)
        {
            InfractorId = output.InfractorId;
        }

        public void WriteError(string message)
        {
            throw new System.NotImplementedException();
        }
    }
}
