using System;
using TinyService.Command;
using TinyService.Command.Impl;
namespace TinyService.Service
{
    public interface IServiceBus
    {
        void Publish<T>(T message) where T : TinyService.DomainEvent.IDomainEvent;
        //CommandResponse Send<TCommand>(TCommand command, int timeoutmilliseconds = 10000) where TCommand : class, TinyService.Command.ICommand;
        System.Threading.Tasks.Task<CommandResult> SendAsync<TCommand>(TCommand command, int timeoutmilliseconds = 10000) where TCommand : class, TinyService.Command.ICommand;

        IDisposable RegisterCommandType<TCommand>() where TCommand : class,ICommand;
         IDisposable ToSubscribe<T>(IObserver<T> handler) where T : TinyService.DomainEvent.IDomainEvent;
        IDisposable ToSubscribe<T>(Action<T> action) where T : TinyService.DomainEvent.IDomainEvent;
    }
}
