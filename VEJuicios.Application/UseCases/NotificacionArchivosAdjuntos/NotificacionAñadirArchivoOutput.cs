using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VEJuicios.Application.UseCases.NotificacionArchivosAdjuntos
{
    public sealed class NotificacionAñadirArchivoOutput : Output
    {
        public NotificacionAñadirArchivoOutput(int archivoId)
        {
            ArchivoId = archivoId;
        }

        public int ArchivoId { get; }
    }
}
