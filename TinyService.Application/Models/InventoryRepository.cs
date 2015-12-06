using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Domain.Repository;

namespace TinyService.Application.Models
{
    public class InventoryRepository : AbstractRepository<string, InventoryItem>
    {
        static ConcurrentDictionary<string, InventoryItem> store = new ConcurrentDictionary<string, InventoryItem>();
        public override void Delete(string id)
        {
            InventoryItem value;
            store.TryRemove(id, out value);
        }

        public override void Delete(InventoryItem entity)
        {
            Delete(entity.Id);
        }

        public override IQueryable<InventoryItem> GetAll()
        {
            return store.Values.AsQueryable();
        }

        public override InventoryItem Insert(InventoryItem entity)
        {
            store.TryAdd(entity.Id,entity);
            return entity;
        }

        public override InventoryItem Update(InventoryItem entity)
        {
            store[entity.Id] = entity;
            return entity;
        }

          
    }
}
