using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Domain.Entities;
using TinyService.Domain.Repository;
using TinyService.DomainEvent;
using TinyService.Infrastructure;

namespace TinyService.Command
{
    public class CommandUnitOfWork 
    {
        protected readonly List<IDomainEvent> Events = new List<IDomainEvent>();

        protected static readonly ConcurrentDictionary<string, IAggregateRoot> CachedAggregateRoots = new  ConcurrentDictionary<string, IAggregateRoot>();
 
        public IEnumerable<IDomainEvent> EmittedEvents
        {
            get { return Events; }
        }

        public void AddEmittedEvent<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot : IAggregateRoot
        {
             Events.AddRange(aggregateRoot.GetUncommittedChanges());
           
             aggregateRoot.MarkChangesAsCommitted();
        }

        public void AddToCache<TAggregateRoot>(string aggregateRootId, TAggregateRoot aggregateRoot) where TAggregateRoot:IAggregateRoot
        {
             CachedAggregateRoots.GetOrAdd(aggregateRootId,aggregateRoot);
        }

        public bool Exists(string aggregateRootId)
        {
            return CachedAggregateRoots.Any(p => p.Key == aggregateRootId);
        }

        public IAggregateRoot Get<TAggregateRoot>(string aggregateRootId, bool createIfNotExists)
        {
            var aggregateRootInfoFromCache = GetAggregateRootFromCache(aggregateRootId);

            if (aggregateRootInfoFromCache != null)
            {
                return aggregateRootInfoFromCache;
            }
           
            return null;
        }

        public event Action Committed;

        IAggregateRoot GetAggregateRootFromCache(string aggregateRootId)
        {
            if (!CachedAggregateRoots.ContainsKey(aggregateRootId)) return null;
 
            var aggregateRoot = CachedAggregateRoots[aggregateRootId];

            return aggregateRoot;
        }

        public void RaiseCommitted()
        {
            var committed = Committed;

            if (committed != null)
            {
                committed();
            }
        }
    }
}
