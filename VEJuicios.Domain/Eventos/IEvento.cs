using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VEJuicios.Domain.Eventos
{
    public interface IEvento
    {
        string Clase { get; }
        Guid EntityId { get; }
        DateTime? Fecha { get; }
        string Usuario { get; }
        string Comentario { get; }
    }
}
