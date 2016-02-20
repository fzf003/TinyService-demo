using Products_Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products_Core.Ports.Commands
{
    public class ChangeProductCommand : BaseCommand<Product>
    {
        public ChangeProductCommand(string productId, string productName, string productDescription, double price)
            
        {
            ProductId = productId;
            ProductName = productName;
            ProductDescription = productDescription;
            Price = price;
            this.AggregateRootId = productId;
        }

        public string ProductId { get; private set; }
        public string ProductName { get; private set; }
        public string ProductDescription { get; private set; }
        public double Price { get; private set; }
    }
}
