using Products_Core.Ports.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.DomainEvent;
using TinyService.Infrastructure;

namespace Products_Core.Ports.Handlers
{
     [Component(IsSingleton = false)]
    public class ProductAddedEventHandler : IDomainEventHandler<ProductAddedEvent>,
                                            IDomainEventHandler<ProductChangedEvent>,
                                            IDomainEventHandler<ProductRemovedEvent>
    {

        public async void Handle(ProductAddedEvent @event)
        {
            Console.WriteLine("Event:新建商品:{0}",@event.ProductId + "|" + @event.ProductName + "|" + @event.ProductDescription);
        }

        public async void Handle(ProductChangedEvent @event)
        {
            Console.WriteLine("Event:修改商品:{0}",  @event.ProductName + "|" + @event.ProductDescription);
        }

        public async void Handle(ProductRemovedEvent @event)
        {
            Console.WriteLine("Event:删除商品:{0}", @event.ProductId + "|" + @event.Id);
        }
    }
}
