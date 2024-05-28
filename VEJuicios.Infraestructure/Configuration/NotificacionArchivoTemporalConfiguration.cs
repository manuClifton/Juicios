using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VEJuicios.Domain.Model.Notificaciones;

namespace VEJuicios.Infraestructure.Configuration
{
    public class NotificacionArchivoTemporalConfiguration : IEntityTypeConfiguration<NotificacionArchivoTemporal>
    {
        public void Configure(EntityTypeBuilder<NotificacionArchivoTemporal> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            builder.ToTable("NotificacionArchivoTemporal");
            builder.HasKey(x => x.ArchivoId);
        }
    }
}
