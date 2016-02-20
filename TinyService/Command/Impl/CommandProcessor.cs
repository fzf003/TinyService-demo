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
    [Component(IsSingleton = true)]
    public class CommandProcessor : ICommandProcessor
    {
        readonly ICommandMapper _commandMapper;

        public CommandProcessor()
            : this(new DefaultCommandMapper())
        { }

        public CommandProcessor(ICommandMapper commandmapper)
        {
            this._commandMapper = commandmapper;
        }

        public void ProcessCommand<TCommand>(TCommand command)
            where TCommand : class,ICommand
        {
            var emittedDomainEvents = new List<IDomainEvent>();
            try
            {
                var unitOfWork = new CommandUnitOfWork();

                var eventsFromThisUnitOfWork = InnerProcessCommand(unitOfWork, command).ToList();

                unitOfWork.RaiseCommitted();

                eventsFromThisUnitOfWork.Select(p => ObjectFactory.GetService<IEventPublisher>().Dispatch(p)).ToArray();
            }
            catch (Exception exception)
            {
                //Console.WriteLine(exception.Message);
                throw exception;
            }
        }

        IEnumerable<IDomainEvent> InnerProcessCommand<TCommand>(CommandUnitOfWork unitOfWork, TCommand command)
             where TCommand : class,ICommand
        {
            var handler = _commandMapper.GetCommandAction<TCommand>();

            var context = new DefaultCommandContext(unitOfWork);

            handler.Handle(context, command);

            context.GetTrackedAggregateRoots()
                   .ForEach(aggregate =>
                   {
                       unitOfWork.AddEmittedEvent(aggregate);
                   });

            var emittedEvents = unitOfWork.EmittedEvents;

            if (!emittedEvents.Any()) return emittedEvents;

            return emittedEvents;
        }

        internal event Action Disposed = delegate { };

        bool _disposed;

        ~CommandProcessor()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {

                _disposed = true;

                Disposed();
            }
        }
    }
}
