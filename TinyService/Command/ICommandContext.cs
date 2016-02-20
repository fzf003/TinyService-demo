using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Domain.Entities;

namespace TinyService.Command
{
    public interface ICommandContext
    {

        TAggregateRoot Create<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot :class, IAggregateRoot;

        TAggregateRoot Get<TAggregateRoot>(string aggregateRootId) where TAggregateRoot : class,IAggregateRoot;

        List<IAggregateRoot> GetTrackedAggregateRoots();

     }
}
