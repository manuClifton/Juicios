using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VEJuicios.Domain.Model.Notificaciones;

namespace VEJuicios.Infraestructure.Configuration
{
    public class NotificacionArchivoTemporalRelacionConfiguration : IEntityTypeConfiguration<NotificacionArchivoTemporalRelacion>
    {
        public void Configure(EntityTypeBuilder<NotificacionArchivoTemporalRelacion> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            builder.ToTable("NotificacionArchivoTemporalRelacion");
            builder.HasKey(x => x.ArchivoId);
        }
    }
}
