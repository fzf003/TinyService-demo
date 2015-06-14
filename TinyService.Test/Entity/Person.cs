using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Domain.Entities;

namespace TinyService.Test.Entity
{
    public  class Person : Entity<int>
    {
        public Person()
        {
            this.Products1 = new List<Product>();
        }

        //public int Id { get; set; }
        public string PersonName { get; set; }
        public byte[] Products { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Product> Products1 { get; set; }
    }

    public  class Product : Entity<int>
    {
        //public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> Person_id { get; set; }
        public virtual Person Person { get; set; }
    }
}
