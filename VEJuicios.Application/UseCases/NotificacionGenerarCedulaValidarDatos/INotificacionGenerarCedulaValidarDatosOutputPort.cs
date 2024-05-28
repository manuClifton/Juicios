using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VEJuicios.Application.UseCases.NotificacionGenerarCedulaValidarDatos
{
    public interface INotificacionGenerarCedulaValidarDatosOutputPort : IOutputPortStandard<string>,
        IOutputPortError, IOutputPortNotFound
    {
    }
}
