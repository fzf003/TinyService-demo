using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Command;
using TinyService.DomainEvent;
using TinyService.Infrastructure;
using System.Reactive.Linq;
using System.Reactive;
using TinyService.Domain.Entities;
using System.Reactive.Concurrency;
using TinyService.Command.Impl;
namespace TinyService.Service
{
    [Component(IsSingleton = true)]
    public class DefaultServiceBus : IServiceBus
    {
        private readonly TinyService.Command.ICommandDispenser _commandDispenser;

        private readonly IEventPublisher _eventpublisher;

        private readonly CommandResultProcessor _commandResultProcessor;


        public DefaultServiceBus(TinyService.Command.ICommandDispenser commandDispenser, IEventPublisher eventpublisher)
        {
            this._commandDispenser = commandDispenser;
            this._eventpublisher = eventpublisher;
            this._commandResultProcessor = ObjectFactory.GetService<CommandResultProcessor>();// new CommandResultProcessor();
        }

        public void Dispose()
        {
            if (this._commandDispenser != null)
            {
                this._commandDispenser.Dispose();
            }

            if (this._eventpublisher != null)
            {
                this._eventpublisher.Dispose();
            }

        }



        public  Task<CommandResult> SendAsync<TCommand>(TCommand command, int timeoutmilliseconds = 10000) where TCommand : class, ICommand
        {
            var task = new TaskCompletionSource<CommandResult>();
            _commandResultProcessor.RegisterProcessingCommand(command, task);
             this._commandDispenser.SendAsync(command);
             return task.Task;
         }



        public IDisposable RegisterCommandType<TCommand>() where TCommand : class, ICommand
        {
            return this._commandDispenser.GetMessages().RegisterCommand<TCommand>(ObjectFactory.GetService<ICommandProcessor>());
        }



        public void Publish<T>(T message) where T : IDomainEvent
        {
            this._eventpublisher.Dispatch(message);
        }

        public IDisposable ToSubscribe<T>(IObserver<T> handler) where T : IDomainEvent
        {
            if (handler == null)
            {
                throw new ArgumentNullException("not is handle");
            }

            return this._eventpublisher
                       .GetMessages()
                       .OfType<T>()
                       .Subscribe(handler);
        }


        public IDisposable ToSubscribe<T>(Action<T> action) where T : IDomainEvent
        {
            return this.ToSubscribe<T>(new Handler<T>(action));
        }
    }
}
