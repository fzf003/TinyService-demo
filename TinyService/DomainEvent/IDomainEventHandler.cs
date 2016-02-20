using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyService.DomainEvent
{
    public interface IDomainEventHandler<TDomainEvent>
         where TDomainEvent : class,IDomainEvent
    {
        void Handle(TDomainEvent @event);
    }


}
