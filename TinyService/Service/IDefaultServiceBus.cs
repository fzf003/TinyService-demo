using System;
namespace TinyService.Service
{
    public interface IDefaultServiceBus
    {
        void Publish<T>(T message) where T : TinyService.DomainEvent.IDomainEvent;
        TinyService.Command.CommandExecuteResult Send<TCommand>(TCommand command, int timeoutmilliseconds = 10000) where TCommand : class, TinyService.Command.ICommand;
        System.Threading.Tasks.Task<TinyService.Command.CommandExecuteResult> SendAsync<TCommand>(TCommand command, int timeoutmilliseconds = 10000) where TCommand : class, TinyService.Command.ICommand;
        IDisposable ToSubscribe<T>(IObserver<T> handler) where T : TinyService.DomainEvent.IDomainEvent;
    }
}
