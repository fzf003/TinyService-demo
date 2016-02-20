using Products_Core.Model;
using Products_Core.Ports.Commands;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Command;
using TinyService.Domain.Entities;
using TinyService.Domain.Repository;
using TinyService.Infrastructure;
using TinyService.Extension.Repository;
namespace Products_Core.Ports.Handlers
{

    [Component(IsSingleton = true)]
    public class ProductCommandHandler : ICommandHandler<AddProductCommand>,
                                         ICommandHandler<ChangeProductCommand>,
                                         ICommandHandler<RemoveProductCommand>
    {
        public void Handle(ICommandContext context, AddProductCommand command)
        {
            //throw new Exception("dsds");
           context.Create<Product>(new Product("1", command.ProductName, command.ProductDescription, command.ProductPrice));
        }

        public void Handle(ICommandContext context, ChangeProductCommand command)
        {
            Product product = context.Get<Product>(command.AggregateRootId);
            Task.Delay(1000).Wait();
            
            if (product == null)
            {
                throw new Exception("该商品不存在");
            }

            product.Changed(command.ProductName, command.ProductDescription, command.Price);
        }

        public void Handle(ICommandContext context, RemoveProductCommand command)
        {
             context.Get<Product>("1").Remove();
        }
    }
}
