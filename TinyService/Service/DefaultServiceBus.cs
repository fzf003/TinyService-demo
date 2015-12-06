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
namespace TinyService.Service
{
    [Component(IsSingleton=true)]
    public class DefaultServiceBus : TinyService.Service.IDefaultServiceBus
    {
        private ICommandService _commandservice;
        private readonly IDomainEventPublisher _eventpublisher;
         
        public DefaultServiceBus(ICommandService commandservice, IDomainEventPublisher eventpublisher)
        {
            this._commandservice = commandservice;
            this._eventpublisher = eventpublisher;
            this._eventpublisher.Start();
            
         }
  
        public void Dispose()
        {
            if(this._commandservice!=null)
            {
                this._commandservice.Dispose();
            }

            if (this._eventpublisher != null)
            {
                this._eventpublisher.Dispose();
            }
            
        }

        public CommandExecuteResult Send<TCommand>(TCommand command, int timeoutmilliseconds=10000) where TCommand : class, ICommand
        {

            return this._commandservice.Send(command, CommitEvents, timeoutmilliseconds);
        }

        public Task<CommandExecuteResult> SendAsync<TCommand>(TCommand command, int timeoutmilliseconds=10000) where TCommand : class, ICommand
        {

            return this._commandservice.SendAsync(command, CommitEvents, timeoutmilliseconds);
        }

        private void CommitEvents(IEnumerable<IDomainEvent> events)
        {
            if (events.Any())
            {
                foreach (var domainevent in events)
                {
                    _eventpublisher.Dispatch(domainevent);
                }
             }
           
        }

        public void Publish<T>(T message) where T:IDomainEvent
        {
            this._eventpublisher.Dispatch(message);
        }

        public IDisposable ToSubscribe<T>(IObserver<T> handler) where T : IDomainEvent
        {
            if(handler==null)
            {
                throw new ArgumentNullException("not is handle");
            }

            return this._eventpublisher.GetDomainEvents()
                       .OfType<T>()
                       .Subscribe(handler);
        }
    }
}
