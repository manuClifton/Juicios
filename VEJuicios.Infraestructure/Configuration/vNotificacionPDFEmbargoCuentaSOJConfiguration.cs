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
    public class vNotificacionPDFEmbargoCuentaSOJConfiguration : IEntityTypeConfiguration<vNotificacionPDFEmbargoCuentaSOJ>
    {
        public void Configure(EntityTypeBuilder<vNotificacionPDFEmbargoCuentaSOJ> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            builder.ToView("vNotificacionPDFEmbargoCuentaSOJ");
            builder.HasKey(x => x.NotificacionId);
        }
    }
}
