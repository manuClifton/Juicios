using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VEJuicios.Domain.Eventos
{
    public interface IEventoRepository : IRepository<Evento>
    {
        IQueryable<Evento> FindByEntityId(Guid entityId);
    }
}
