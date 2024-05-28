using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VEJuicios.Application.UseCases.NotificacionArchivosAdjuntos;

namespace VEJuicios.Application.UseCases.NotificacionVerificarMegas
{
    public interface INotificacionVerigficarMegasPresenterOutputPort : IOutputPortStandard<bool>,
        IOutputPortError, IOutputPortNotFound
    {
    }
}
