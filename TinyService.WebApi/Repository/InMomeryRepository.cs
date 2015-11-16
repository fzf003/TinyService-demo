using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TinyService.Domain.Repository;
using TinyService.Infrastructure;
using TinyService.WebApi.Domain;

namespace TinyService.WebApi.Repository
{
     [Component(IsSingleton=true)]
    public class InMomeryRepository : AbstractRepository<string, Manager>
    {
        static IDictionary<string, Manager> store = new ConcurrentDictionary<string, Manager>();

        public override void Delete(string id)
        {
            Delete(store[id]);
        }
        public override void Delete( Manager entity)
        {
            store.Remove(entity.ID);
        }

        public override Manager Insert(Manager entity)
        {
            store[entity.ID] = entity;
            return entity;
        }

        public override Manager Update(Manager entity)
        {
            store[entity.ID] = entity;
            return entity;
        }

        public override IQueryable<Manager> GetAll()
        {
            return store.Values.AsQueryable();
        }
    }
}