using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace VEJuicios.Domain
{
    public interface IDomainEvent : INotification
    {
        DateTime FechaHoraEvento { get; }
        string Usuario { get; }
        Guid GetAggregateId();
    }
}

