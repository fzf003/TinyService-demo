using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products_Core.Ports.Events
{
    public class ProductChangedEvent : BaseDomainEvent
    {
        public string ProductDescription { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }

        public ProductChangedEvent(string productName, string productDescription, double productPrice)
        {

            ProductName = productName;
            ProductDescription = productDescription;
            ProductPrice = productPrice;
        }
    }
}
