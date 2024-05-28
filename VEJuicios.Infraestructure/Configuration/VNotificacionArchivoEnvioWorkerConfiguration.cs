using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VEJuicios.Domain.Model.Notificaciones;

namespace VEJuicios.Infraestructure.Configuration
{
    public class VNotificacionArchivoEnvioWorkerConfiguration : IEntityTypeConfiguration<VNotificacionArchivoEnvioWorker>
    {
        public void Configure(EntityTypeBuilder<VNotificacionArchivoEnvioWorker> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            builder.ToView("vNotificacionArchivoEnvioWorker");
            builder.HasKey(x => x.ArchivoId);
        }
    }
}
