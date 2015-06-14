using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Test.Entity;
using TinyService.Test.Entity.Map;

namespace TinyService.Test.Repository
{
    public partial class pmContext : DbContext
    {
        static pmContext()
        {
            Database.SetInitializer<pmContext>(null);
        }
 
        public pmContext()
            : base("Name=pmContext")
        {
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PersonMap());
            modelBuilder.Configurations.Add(new ProductMap());
        }
    }
}
