using Products_Core.Ports.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Domain.Entities;

namespace Products_Core.Model
{
    public class Product : AggregateRoot<string>
    {

        public Product(string Id, string productName, string productDescription, double productPrice) :
            base(Id)
        {
            Console.WriteLine("新建:{0}", Id);

            ApplyChange(new ProductAddedEvent(Id, productName, productDescription, productPrice));
        }

        public void Changed(string productName, string productDescription, double productPrice)
        {
            Console.WriteLine("修改:{0}-{1}-{2}", Id, productName, productDescription);

            ApplyChange(new ProductChangedEvent( productName, productDescription, productPrice));
        }

        public void Remove()
        {
             ApplyChange(new ProductRemovedEvent(this.Id));
        }

        protected void Apply(ProductRemovedEvent e)
        {
            Console.WriteLine("删除:{0}",e.Id+"-"+e.ProductId);
            this.IsDelete = true;
        }

        protected void Apply(ProductAddedEvent e)
        {
            ProductName = e.ProductName;
            ProductDescription = e.ProductDescription;
            ProductPrice = e.ProductPrice;
        }

        protected void Apply(ProductChangedEvent e)
        {
            ProductName = e.ProductName;
            ProductPrice = e.ProductPrice;
            ProductDescription = e.ProductDescription;
        }
        

        public string ProductDescription { get; set; }
        public bool IsDelete { get; set; }

        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
    }
}
