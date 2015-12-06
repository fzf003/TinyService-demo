using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyService.Test.Entity.Map
{
    public class PersonMap : EntityTypeConfiguration<Person>
    {
        public PersonMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.PersonName)
                .HasMaxLength(255);

            this.Property(t => t.Title)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("Person");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.PersonName).HasColumnName("PersonName");
            this.Property(t => t.Products).HasColumnName("Products");
            this.Property(t => t.Title).HasColumnName("Title");
        }
    }

    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("Product");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Person_id).HasColumnName("Person_id");

            // Relationships
            this.HasOptional(t => t.Person)
                .WithMany(t => t.Products1)
                .HasForeignKey(d => d.Person_id);

        }
    }
}
