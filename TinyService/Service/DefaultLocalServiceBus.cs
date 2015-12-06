using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using TinyService.Domain.Entities;
using TinyService.Infrastructure;
using TinyService.Infrastructure.RegisterCenter;

namespace TinyService.Service
{

    [Component(IsSingleton = true)]
    public class DefaultLocalServiceBus :IServiceBus
    {
        private readonly IDictionary<string, object> _messagehandler;

        private readonly EventHandlerRegistry _eventsobservables = EventHandlerRegistry.Instance;

        private CompositeDisposable disposables;

        public DefaultLocalServiceBus()
        {
            _messagehandler = new ConcurrentDictionary<string, object>();
            disposables = new CompositeDisposable();
        }

        public IDisposable RegisterMessageHandler<TCommand>(Action<TCommand> handler) where TCommand : class
        {
            var messagehandler = new Handler<TCommand>(handler);

            return this.RegisterMessageHandler<TCommand>(messagehandler);
        }

        public IDisposable RegisterMessageHandler<TCommand>(IObserver<TCommand> observer) where TCommand : class
        {
           
            this._messagehandler[typeof(TCommand).Name] = observer;

            var disposable = Disposable.Create(() =>
            {
                var observerhandler = (this._messagehandler[typeof(TCommand).Name] as IObserver<TCommand>);
                observerhandler.OnCompleted();
                this._messagehandler.Remove(typeof(TCommand).Name);
            });

            disposables.Add(disposable);
            return disposable;
        }

        public void Send<TCommand>(TCommand message) where TCommand : class
        {
            var typename = message.GetType().Name;

            object observer;

            if (this._messagehandler.TryGetValue(typename, out observer))
            {
                ((IObserver<TCommand>)observer).OnNext(message);
            }
        }

        public void Publish<T>(T message)
        {
            foreach(var item in this._eventsobservables.GetEventHandler(typeof(T)))
            {
                  if (item != null)
                {
                    var observer = (ISubject<T>)item;
                    observer.OnNext(message);
                }
            }
            //Parallel.ForEach(this._eventsobservables.GetEventHandler(typeof(T)), (item) =>
            //{
            //    if (item != null)
            //    {
            //        var observer = (ISubject<T>)item;
            //        observer.OnNext(message);
            //    }
            //});
        }

        public IDisposable Subscribe<T>(IObserver<T> handler)
        {
            var subject = new Subject<T>();
            this._eventsobservables.AddEventHandler(typeof(T), subject);
            var disp=subject.Subscribe(handler);
            disposables.Add(disp);
            return disp;
        }

 
        public void Dispose()
        {
            disposables.Dispose();
            this._eventsobservables.Subjects.Clear();
            _messagehandler.Clear();
         }
    }
}
