using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Infrastructure;

namespace TinyService.Domain.Entities
{
    //public interface IDomainRepository
    //{
    //    T Get<T>(object aggregateRootId) where T : class, IAggregateRoot;
    //    void Save(object aggregateRootId, IAggregateRoot AggregateRoot);
        
    //}

    public interface IDomainStore<TKey, TValue> where TValue:class,IAggregateRoot
    {
        void Save(TKey key, TValue value);

        TValue Get(TKey key);

       IAggregateRoot Get(Type aggregateRootType, string aggregateRootId);
    }
 
    public class InMemoryDomainStore<TKey, TValue> : IDomainStore<TKey, TValue>
        where TValue : class,IAggregateRoot
    {
        static ConcurrentDictionary<TKey, TValue> store = new ConcurrentDictionary<TKey, TValue>();
 
        public void Save(TKey key, TValue value)
        {
            store.AddOrUpdate(key, value, (oldkey, old) =>
            {
                return value;
            });
        }
 
        public TValue Get(TKey key)
        {
            TValue value;
            store.TryGetValue(key, out value);
            return value;
        }


        public IAggregateRoot Get(Type aggregateRootType, string aggregateRootId)
        {
            var key = store.Keys.FirstOrDefault(p => p.Equals(aggregateRootId));
            return store[key];
            //store.Values.Select(p=>p.GetType()==aggregateRootType).
        }
    }



   /* public class DefaultDomainRepository : IDomainRepository
    {
        public static ConcurrentDictionary<string, IAggregateRoot> store = new ConcurrentDictionary<string, IAggregateRoot>();
        public DefaultDomainRepository()
        {

        }
        public T Get<T>(object aggregateRootId) where T : class, IAggregateRoot
        {
            IAggregateRoot aggregateRoot = null;
            
            if(store.TryGetValue(aggregateRootId.ToString(), out aggregateRoot))
            {
                return (T)aggregateRoot;
            }

            return (T)aggregateRoot;
        }


        public void Save(object aggregateRootId, IAggregateRoot AggregateRoot)
        {
            store.AddOrUpdate(aggregateRootId.ToString(), AggregateRoot, (key, old) => {
                return AggregateRoot;
            });
        }
    }*/
}
