using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VEJuicios.Domain.Eventos
{
    public class Evento : Entity<Evento>, IEvento
    {
        public string Clase { get; private set; }
        public Guid EntityId { get; private set; }
        public DateTime? Fecha { get; private set; }
        public string Usuario { get; private set; }
        public string Comentario { get; private set; }

        public Evento()
        {
        }

        private Evento(string clase, Guid entityId, DateTime fecha, string usuario, string comentario)
        {
            Clase = clase;
            EntityId = entityId;
            Fecha = fecha;
            Usuario = usuario;
            Comentario = comentario;
        }

        public static Evento Create(string clase, Guid entityId, DateTime fecha,
            string usuario, string comentario)
        {
            return new Evento(clase, entityId, fecha, usuario, comentario);
        }

    }
}
