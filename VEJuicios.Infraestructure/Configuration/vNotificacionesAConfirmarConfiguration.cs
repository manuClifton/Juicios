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
    public class vNotificacionesAConfirmarConfiguration : IEntityTypeConfiguration<vNotificacionesAConfirmar>
    {
        public void Configure(EntityTypeBuilder<vNotificacionesAConfirmar> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            builder.ToView("vNotificacionesAConfirmar");
            builder.HasKey(x => x.ArchivoTemporalRelacionadoArchivoId);
        }
    }
}
