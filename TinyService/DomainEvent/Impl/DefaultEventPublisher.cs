using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Infrastructure;

namespace TinyService.DomainEvent.Impl
{
    [Component(IsSingleton = true)]
    public class DefaultEventPublisher : MessageDispenser<IDomainEvent>, IEventPublisher
    {

        public async Task<bool> Dispatch(IDomainEvent @event)
        {
            return await this.SendAsync(@event);
        }
    }
}
