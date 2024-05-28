using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VEJuicios.Application.UseCases.GenerarNotificacion;

namespace VEJuicios.Application.UseCases.NotificacionArchivosAdjuntos
{
    public interface INotificacionAñadirArchivoPresenterOutputPort : IOutputPortStandard<NotificacionAñadirArchivoOutput>,
        IOutputPortError, IOutputPortNotFound
    {
    }
}
