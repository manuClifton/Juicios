using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

using VEJuicios.Domain.Eventos;

namespace VEJuicios.Domain
{
    public abstract class Entity<T> : IEntity
    {
        public string Id { get; protected set; }

        public Entity(string id = null)
        {
            Id = id ?? Guid.NewGuid().ToString();
        }

        public List<IEvento> _eventos = new();

        public List<IEvento> Eventos => _eventos;

        public void AgregarEvento(IEvento evento)
        {
            this._eventos.Add(evento);
        }

        public bool Equals(Entity<T> other)
        {
            if (other == null
                || !ReferenceEquals(other, this)
                || other.GetType() != typeof(T))
            {
                return false;
            }

            return Id.Equals(other.Id);
        }
    }
}
