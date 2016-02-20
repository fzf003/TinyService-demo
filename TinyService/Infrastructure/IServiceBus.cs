//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TinyService.Command;

//namespace TinyService.Infrastructure
//{
//    public interface IServiceBus:IDisposable
//    {
//        //CommandExecuteResult Send<TCommand>(TCommand message, int timeoutmilliseconds) where TCommand : class,ICommand;
//        //Task<CommandExecuteResult> SendAsync<TCommand>(TCommand message, int timeoutmilliseconds) where TCommand : class,ICommand;

//        void Send<TCommand>(TCommand message) where TCommand : class;
//        IDisposable RegisterMessageHandler<TCommand>(Action<TCommand> handler) where TCommand : class;
//        IDisposable RegisterMessageHandler<TCommand>(IObserver<TCommand> observer) where TCommand : class;

//        void Publish<T>(T message);
//        IDisposable Subscribe<T>(IObserver<T> handler);

//    }
//}
