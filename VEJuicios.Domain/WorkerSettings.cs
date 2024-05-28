using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VEJuicios.Domain
{
    public class WorkerSettings
    {
        public WorkerConfig WorkerRecepcionAfip { get; set; }
        public WorkerConfig WorkerEnvioAfip { get; set; }
        public WorkerConfig WorkerLimpiarArchivosNotificaciones { get; set; }
    }
}
