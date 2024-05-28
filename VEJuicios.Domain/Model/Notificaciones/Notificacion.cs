using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VEJuicios.Domain.Model.JuiciosNotificaciones
{
    public partial class Notificacion
    {
        [Key]
        public int NotificacionId { get; set; }
        public decimal NumeroInterno { get; set; }
        public decimal CasoId { get; set; }
        public long PersonaId { get; set; }
        public long ParteId { get; set; }
        public EnumEstadoNotificaciones EstadoId { get; set; }
        public decimal TipoNotificacionId { get; set; }
        public decimal? NumeroAsociado { get; set; }
        public decimal MetodoEnvioId { get; set; }
        public DateTime FechaElaboracion { get; set; }
        public DateTime? FechaConfirmacion { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public DateTime? FechaRecepcionAfip { get; set; }
        public DateTime? FechaNotificacion { get; set; }
        public DateTime FechaInsert { get; set; }
        public DateTime FechaUpdate { get; set; }
        public int UsuarioInsertId { get; set; }
        public int UsuarioUpdateId { get; set; }
        public Guid? AfipId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public byte[] Stamp { get; set; }
        public DateTime? FechaCancelacion { get; set; }
        public Notificacion() { }
    }
}
