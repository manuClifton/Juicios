using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VEJuicios.Domain.Model.Notificaciones;
using VEJuicios.Domain;

namespace VEJuicios.Domain.Model.Notificaciones
{
    public interface INotificacionAdjuntoEnviadoRepository : IRepository<NotificacionAdjuntoEnviado>
    {
        Task AddAsync(NotificacionAdjuntoEnviado entity);
    }
}