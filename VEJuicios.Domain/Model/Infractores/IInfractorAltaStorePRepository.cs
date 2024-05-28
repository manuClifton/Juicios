namespace VEJuicios.Domain.Model.Infractores
{
    public interface IInfractorAltaStorePRepository
    {
        Task<int> AltaInfractor(string xmlData);
    }
}
