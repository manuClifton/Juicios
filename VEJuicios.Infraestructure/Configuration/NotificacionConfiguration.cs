using VEJuicios.Domain.Model.JuiciosNotificaciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VEJuicios.Domain;

namespace VEJuicios.Infraestructure.Configuration
{
    public class NotificacionConfiguration : IEntityTypeConfiguration<Notificacion>
    {
        public void Configure(EntityTypeBuilder<Notificacion> entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            entity.ToTable("Notificacion");
            entity.HasKey(x => x.NotificacionId);
            entity.ToTable(tb => tb.HasTrigger("tr_actualizacion_estadoNotificacion"));

            entity.Property(e => e.CasoId)
            .HasColumnType("numeric(18, 0)");

            entity.Property(e => e.NumeroInterno)
            .HasColumnType("numeric(18, 0)");

            entity.Property(e => e.MetodoEnvioId)
            .HasColumnType("numeric(18, 0)");

            entity.Property(e => e.NumeroAsociado)
            .HasColumnType("numeric(18, 0)");

            entity.Property(e => e.TipoNotificacionId)
            .HasColumnType("numeric(18, 0)");

        }
    }
}