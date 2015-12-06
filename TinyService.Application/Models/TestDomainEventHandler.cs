using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TinyService.Application.Models.DomainEvent;
using TinyService.Domain.Repository;
using TinyService.DomainEvent;
using TinyService.Infrastructure;

namespace TinyService.Application.Models
{
    [Component]
    public class TestDomainEventHandler : IDomainEventHandler<InventoryItemCreated>,
                                          IDomainEventHandler<InventoryItemRenamed>

    {
        int index = 0;
        private readonly IRepository<string, InventoryItem> _domainrepository;
        public TestDomainEventHandler(IRepository<string, InventoryItem> domainrepository)
        {
            this._domainrepository = domainrepository;
        }
        public void Handle(InventoryItemCreated @event)
        {
             
            Console.WriteLine("第{0}个:Created:{1}",Interlocked.Increment(ref index),@event.Name+"-"+@event.Timestamp+"-T-"+Thread.CurrentThread.ManagedThreadId);
        }

        public void Handle(InventoryItemRenamed @event)
        {
            //Thread.Sleep(1000);
            Console.WriteLine("第{0}个:Renamed:{1}", Interlocked.Increment(ref index),  @event.NewName + "----" + @event.Timestamp + "-T-" + Thread.CurrentThread.ManagedThreadId);

        }
    }
}
