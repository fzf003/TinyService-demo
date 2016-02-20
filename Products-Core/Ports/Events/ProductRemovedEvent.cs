using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products_Core.Ports.Events
{
    public class ProductRemovedEvent : BaseDomainEvent
    {
        public string ProductId { get; set; }
        //public string ProductName { get; set; }
        //public string ProductDescription { get; set; }
        //public double ProductPrice { get; set; }

        public ProductRemovedEvent(string productId)
             
        {
            ProductId = productId;
            
        }
    }
}
