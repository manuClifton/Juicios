using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VEJuicios.Domain.Model;
using VEJuicios.Domain.Model.Notificaciones;

namespace VEJuicios.Infraestructure.Configuration
{
    internal class VistaNotificacionArchivoTemporalMetadataConfiguration : IEntityTypeConfiguration<VistaNotificacionArchivoTemporalMetadata>
    {
        public void Configure(EntityTypeBuilder<VistaNotificacionArchivoTemporalMetadata> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            builder.ToView("vNotificacionArchivoTemporalMetadata");
            builder.HasKey(x => x.ArchivoId); 
        }
    }
}
