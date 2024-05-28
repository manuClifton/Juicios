namespace VEJuicios.Domain.Model.Notificaciones
{
    public interface IvNotificacionPDFEmbargoCuentaSOJRepository : IRepository<vNotificacionPDFEmbargoCuentaSOJ>
    {
        List<vNotificacionPDFEmbargoCuentaSOJ> GetById(int Id);
    }
}
