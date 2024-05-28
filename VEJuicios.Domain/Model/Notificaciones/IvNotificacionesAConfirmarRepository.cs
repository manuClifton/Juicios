namespace VEJuicios.Domain.Model.Notificaciones
{
    public interface IvNotificacionesAConfirmarRepository : IRepository<vNotificacionesAConfirmar>
    {
        bool GetCorrectoParaConfirmar(int notificacionId);
        bool GetCorrectoParaGrabarYConfirmar(int notificacionId, int[] relacionadosYARelacionarID, string tipoNotificacion, int cedulaId);
    }
}
