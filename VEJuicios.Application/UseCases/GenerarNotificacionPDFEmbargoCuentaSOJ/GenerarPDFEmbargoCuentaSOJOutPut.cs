using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VEJuicios.Application.UseCases.GenerarNotificacionPDFEmbargoCuentaSOJ
{
    public sealed class GenerarPDFEmbargoCuentaSOJOutPut : Output
    {
        public GenerarPDFEmbargoCuentaSOJOutPut(byte[] datos, int archivoId, string fileName)
        {
            Datos = datos;
            ArchivoId = archivoId;
            FileName = fileName;
        }

        public byte[] Datos { get; }
        public int ArchivoId { get; }
        public string FileName { get; }
    }
}
