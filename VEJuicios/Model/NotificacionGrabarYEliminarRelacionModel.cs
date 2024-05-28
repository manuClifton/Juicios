namespace VEJuicios.Model
{
    public sealed class NotificacionGrabarYEliminarRelacionModel
    {
        public int[] GuardarArchivosID { get; set; }
        public int[] EliminarArchivosID { get; set; }
        public int[] RelacionadosYARelacionarID { get; set; }
        public string TipoNotificacion { get; set; }
        public int NotificacionID { get; set; }
        public int CedulaID { get; set; }
        public int UserId { get; set; }
    }
}
