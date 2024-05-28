using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VEJuicios.Domain
{
    public abstract class AggregateRoot<T> : Entity<T>, IAggregateRoot
    {
        private List<IDomainEvent> _domainEvents = new();

        public List<IDomainEvent> DomainEvents => _domainEvents;

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            // Add the domain event to this aggregate's list of domain events
            this._domainEvents.Add(domainEvent);

            // Log the domain event
            this.LogDomainEventAdded(domainEvent);
        }

        public void ClearEvents()
        {
            this._domainEvents.Clear();
        }

        private void LogDomainEventAdded(IDomainEvent domainEvent)
        {
            Type thisClass = this.GetType();
            Type domainEventClass = domainEvent.GetType();
#if DEBUG
            System.Console.Out.WriteLine($"[Domain Event Created]:{thisClass.FullName}==>{domainEventClass.FullName}");
#endif
        }
    }
}
