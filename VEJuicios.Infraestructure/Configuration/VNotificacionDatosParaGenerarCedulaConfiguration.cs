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
    public class VNotificacionDatosParaGenerarCedulaConfiguration : IEntityTypeConfiguration<VNotificacionDatosParaGenerarCedula>
    {

        public void Configure(EntityTypeBuilder<VNotificacionDatosParaGenerarCedula> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            builder.ToView("vNotificacionDatosParaGenerarCedula");
            builder.HasKey(x => x.NotificacionId);
        }
    }
}
