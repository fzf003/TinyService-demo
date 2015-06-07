using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyService.Infrastructure
{
    public interface IServiceBus:IDisposable
    {
        void Send<TCommand>(TCommand message) where TCommand : class;
        IDisposable RegisterMessageHandler<TCommand>(Action<TCommand> handler) where TCommand : class;
        IDisposable RegisterMessageHandler<TCommand>(IObserver<TCommand> observer) where TCommand : class;
        void Publish<T>(T message);
        IDisposable Subscribe<T>(IObserver<T> handler);

    }
}
