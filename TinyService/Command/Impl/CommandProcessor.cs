using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Text;
using System.Threading.Tasks;
using TinyService.DomainEvent;
using TinyService.Infrastructure;
using TinyService.Service;
using System.Reactive.Linq;
namespace TinyService.Command.Impl
{
    [Component(IsSingleton = false)]
    public class CommandProcessor :Disposable, ICommandProcessor
    {
        readonly ICommandMapper _commandMapper;
 
        public CommandProcessor(ICommandMapper commandmapper)
        {
            this._commandMapper = commandmapper;
        }

        public void ProcessCommand<TCommand>(TCommand command)
            where TCommand : class,ICommand
        {
                var emittedDomainEvents = new List<IDomainEvent>();
           
                var unitOfWork = new CommandUnitOfWork();

                var eventsFromThisUnitOfWork = InnerProcessCommand(unitOfWork, command).ToList();

                unitOfWork.RaiseCommitted();

                eventsFromThisUnitOfWork.Select(p => ObjectFactory.GetService<IEventPublisher>().Dispatch(p)).ToArray();
        }

        IEnumerable<IDomainEvent> InnerProcessCommand<TCommand>(CommandUnitOfWork unitOfWork, TCommand command)
             where TCommand : class,ICommand
        {
            var handler = _commandMapper.GetCommandAction<TCommand>();

            var context = new DefaultCommandContext(unitOfWork);

            handler.Handle(context, command);

            context.GetTrackedAggregateRoots().ForEach(aggregate =>unitOfWork.AddEmittedEvent(aggregate));

            var emittedEvents = unitOfWork.EmittedEvents;

            if (!emittedEvents.Any()) return emittedEvents;

            return emittedEvents;
        }

       
    }
}
