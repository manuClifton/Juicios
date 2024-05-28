using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VEJuicios.Domain.Model.Notificaciones;

namespace VEJuicios.Infraestructure.Configuration
{
    public class NotificacionAdjuntoEnviadoConfiguration : IEntityTypeConfiguration<NotificacionAdjuntoEnviado>
    {
        public void Configure(EntityTypeBuilder<NotificacionAdjuntoEnviado> entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            entity.ToTable("NotificacionAdjuntoEnviado");
            entity.HasKey(x => x.ReferenciaApiId);

            entity.Property(e => e.NotificacionId)
                  .HasColumnType("numeric(18, 0)");
        }
    }
}
