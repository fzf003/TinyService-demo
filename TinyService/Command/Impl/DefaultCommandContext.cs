using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Domain.Entities;
using TinyService.Domain.Repository;
using TinyService.Infrastructure;

namespace TinyService.Command.Impl
{
    public class DefaultCommandContext : ICommandContext
    {
        readonly CommandUnitOfWork _unitOfWork;
        readonly List<IAggregateRoot> _trackedAggregateRoots;
        
        public DefaultCommandContext(CommandUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _trackedAggregateRoots = new List<IAggregateRoot>();
        }


        public TAggregateRoot Create<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot :class, IAggregateRoot
        {
            string aggregateRootId = aggregateRoot.AsDynamic().Id.ToString();

            if (_unitOfWork.Exists(aggregateRootId))
            {
                throw new InvalidOperationException(string.Format("不能创建 {0}聚合根  ID为 {1} 该聚合根实例已存在!",
                    typeof(TAggregateRoot), aggregateRootId));
            }
 
            _unitOfWork.AddToCache(aggregateRootId, aggregateRoot);
  
            if(!_trackedAggregateRoots.Contains(aggregateRoot))
            {
                 _trackedAggregateRoots.Add(aggregateRoot);
            }
            
 
            return aggregateRoot;
        }

        public TAggregateRoot Get<TAggregateRoot>(string aggregateRootId) where TAggregateRoot : class,IAggregateRoot
        {
            var root =_unitOfWork.Get<TAggregateRoot>(aggregateRootId, createIfNotExists: false);
 
             if (!_trackedAggregateRoots.Contains(root))
             {
                 _trackedAggregateRoots.Add(root);
             }
             return root as TAggregateRoot;
         }


        public List<IAggregateRoot> GetTrackedAggregateRoots()
        {
            return this._trackedAggregateRoots;
        }
    }
}
