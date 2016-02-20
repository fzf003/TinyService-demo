using Products_Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products_Core.Ports.Commands
{
    public class AddProductCommand :BaseCommand<Product>
    {
        public AddProductCommand( string productName, string productDescription, double productPrice)
            :base()
         {
            ProductName = productName;
            ProductDescription = productDescription;
            ProductPrice = productPrice;
             
         }

        public string ProductDescription { get; private set; }
        public string ProductName { get; private set; }
        public double ProductPrice { get; set; }
        public string ProductId { get; set; }
    }
}
