namespace VEJuicios.Model
{
    public sealed class NotificacionAltaModel
    {
        public int NumeroInterno { get; set; }
        public int CasoId { get; set; }
        public int PersonaId { get; set; }
        public int ParteId { get; set; }
        public int EstadoId { get; set; }
        public int TipoNotificacionId { get; set; }
        public int NumeroAsociado { get; set; }
        public int MetodoEnvioId { get; set; }
        public int UsuarioInsertId { get; set; }
    }
}
