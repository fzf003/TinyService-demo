using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Domain.Repository;
using TinyService.Test.Entity;

namespace TinyService.Test.Repository
{
    public class UserRepository : AbstractRepository<string, User>
    {
        static IDictionary<string, User> store = new ConcurrentDictionary<string, User>();

        public override void Delete(string id)
        {
            Delete(store[id]);
        }
        public override void Delete(User entity)
        {
            store.Remove(entity.ID);
        }

        public override User Insert(User entity)
        {
            store[entity.ID] = entity;
            return entity;
        }

        public override User Update(User entity)
        {
            store[entity.ID] = entity;
            return entity;
        }

        public override IQueryable<User> GetAll()
        {
            return store.Values.AsQueryable();
        }
    }
}
