using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VEJuicios.Domain.Eventos
{
    internal class EventoService
    {
        protected readonly IEventoRepository _repository;

        public EventoService(
            IEventoRepository repository)
        {
            this._repository = repository;
        }

        public Evento CrearEvento(string clase, Guid entityId,
            DateTime fecha, string usuario, string comentario = null)
        {
            Evento evento = Evento.Create(clase, entityId, fecha, usuario, comentario);

            _repository.Add(evento);

            return evento;
        }
    }
}
