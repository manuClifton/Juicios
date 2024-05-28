using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VEJuicios.Application.UseCases.NotificacionDescargarAdjuntoEnviado
{
    public sealed class NotificacionDescargarAdjuntoEnviadoOutput : Output
    {
        public byte[] Datos { get; }
        public string FileName { get; }
        public NotificacionDescargarAdjuntoEnviadoOutput(byte[] datos, string fileName)
        {
            Datos = datos;
            FileName = fileName;
        }
    }
}
