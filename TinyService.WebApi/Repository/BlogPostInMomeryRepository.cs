using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TinyService.Domain.Repository;
using TinyService.Infrastructure;
using TinyService.WebApi.Models;

namespace TinyService.WebApi.Repository
{
    [Component(IsSingleton = true)]
    public class BlogPostInMomeryRepository : AbstractRepository<int, BlogPost>
    {
        static IDictionary<int, BlogPost> store = new ConcurrentDictionary<int, BlogPost>();

        public override void Delete(int id)
        {
            Delete(store[id]);
        }
        public override void Delete(BlogPost entity)
        {
            store.Remove(entity.ID);
        }

        public override BlogPost Insert(BlogPost entity)
        {
            store[entity.ID] = entity;
            return entity;
        }

        public override BlogPost Update(BlogPost entity)
        {
            store[entity.ID] = entity;
            return entity;
        }

        public override IQueryable<BlogPost> GetAll()
        {
            return store.Values.AsQueryable();
        }
    }
}