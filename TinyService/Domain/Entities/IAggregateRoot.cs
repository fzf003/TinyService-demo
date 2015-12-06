using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.DomainEvent;

namespace TinyService.Domain.Entities
{


    public interface IAggregateRoot<T> : IAggregateRoot
    {
         T Id { get; }
     }

    public interface IAggregateRoot 
    {
        int Version { get; }
        void LoadsFromHistory(IEnumerable<IDomainEvent> history);
        void MarkChangesAsCommitted();

        IEnumerable<IDomainEvent> GetUncommittedChanges();
    }
}
