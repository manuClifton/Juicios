namespace VEJuicios.Application.UseCases.GenerarConstanciaNotificacion
{
    public sealed class GenerarConstanciaNotificacionPDFInput
    {
        public int NotificacionId { get; }

        public GenerarConstanciaNotificacionPDFInput(int notificacionId)
        {
            this.NotificacionId = notificacionId;

        }
    }
}
