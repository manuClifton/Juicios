using VEJuicios.Domain.Model.JuiciosNotificaciones;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VEJuicios.Domain.Model;

namespace VEJuicios.Infraestructure.Configuration
{
    public class VistaNotificacionPDFConfiguration : IEntityTypeConfiguration<VistaNotificacionPDF>
    {
        public void Configure(EntityTypeBuilder<VistaNotificacionPDF> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            builder.ToView("vNotificacionPDF");
            builder.HasKey(x => x.NotificacionId);
        }
    }
}
