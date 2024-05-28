using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VEJuicios.Domain.Model.Notificaciones;

namespace VEJuicios.Infraestructure.Configuration
{
    public class NotificacionConstanciaConfiguration : IEntityTypeConfiguration<VNotificacionConstancia>
    {
        public void Configure(EntityTypeBuilder<VNotificacionConstancia> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            builder.ToView("vNotificacionConstancia");
            builder.HasKey(x => x.NotificacionId);
        }
    }
}
