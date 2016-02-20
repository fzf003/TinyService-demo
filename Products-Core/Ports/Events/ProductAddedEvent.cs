using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products_Core.Ports.Events
{
    public class ProductAddedEvent : BaseDomainEvent
    {
        public string ProductDescription { get; set; }
        public string ProductId { get; set; }
        public double ProductPrice { get; set; }
        public string ProductName { get; set; }

        public ProductAddedEvent(string productId, string productName, string productDescription, double productPrice)
            :base()
        {
            ProductId = productId;
            ProductName = productName;
            ProductDescription = productDescription;
            ProductPrice = productPrice;
        }
    }
}
