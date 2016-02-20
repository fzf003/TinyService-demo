using Products_Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products_Core.Ports.Commands
{
    public class RemoveProductCommand : BaseCommand<Product>
    {
        public RemoveProductCommand(string productId)
            
        {
            ProductId = productId;
        }

        public string ProductId { get; private set; }
    }
}
