using Products_Core.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Domain.Repository;
using TinyService.Infrastructure;

namespace Products_Core.Adapters
{
    [Component(IsSingleton = false)]
    public class ProductRepository : AbstractRepository<string, Product>
    {
        static ConcurrentDictionary<string, Product> store = new ConcurrentDictionary<string, Product>();
        public override void Delete(string id)
        {
            Product value;
            store.TryRemove(id, out value);
        }

        public override void Delete(Product entity)
        {
            Delete(entity.Id);
        }

        public override IQueryable<Product> GetAll()
        {
            return store.Values.AsQueryable();
        }

        public override Product Insert(Product entity)
        {
            store.TryAdd(entity.Id, entity);
            return entity;
        }

        public override Product Update(Product entity)
        {
            store[entity.Id] = entity;
            return entity;
        }


    }
}
