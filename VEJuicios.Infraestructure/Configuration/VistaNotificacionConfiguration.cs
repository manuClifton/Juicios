using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VEJuicios.Domain.Model.JuiciosNotificaciones;
using VEJuicios.Domain.Model.Notificaciones;

namespace VEJuicios.Infraestructure.Configuration
{
    public class VistaNotificacionConfiguration : IEntityTypeConfiguration<VistaNotificacion>
    {
        public void Configure(EntityTypeBuilder<VistaNotificacion> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            builder.ToView("vNotificacionReducida");
            builder.HasKey(x => x.NotificacionId);
        }
    }
}
