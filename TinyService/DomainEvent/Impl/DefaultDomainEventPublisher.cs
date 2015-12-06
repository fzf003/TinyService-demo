using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System.Reactive.Disposables;

namespace TinyService.DomainEvent.Impl
{
    public class DefaultDomainEventPublisher : IDomainEventPublisher
    {
        private const int BoundedCapacity = 1024;
        private readonly BlockingCollection<IDomainEvent> _queue;
        private Task _worker;
        private bool Started = false;
        private ReplaySubject<IDomainEvent> _replaysubjects;
        public DefaultDomainEventPublisher()
        {
            _queue = new BlockingCollection<IDomainEvent>(new ConcurrentQueue<IDomainEvent>(), BoundedCapacity);
            this._replaysubjects = new ReplaySubject<IDomainEvent>();
        }

        public void Start()
        {
            _worker = Task.Factory.StartNew(Working);
            Started = true;
        }

        public bool Dispatch(IDomainEvent @event)
        {
            if (!Started)
            {
                throw new InvalidOperationException("Not Start");
            }
            return _queue.TryAdd(@event);
        }

        private void Working()
        {
            foreach (var @event in _queue.GetConsumingEnumerable())
            {
                   this._replaysubjects.OnNext(@event);
            }

        }



        public IObservable<IDomainEvent> GetDomainEvents()
        {
            var src = _replaysubjects.ObserveOn(TaskPoolScheduler.Default);
             
             return ToEvents(src);
        }

 

        IObservable<T> ToEvents<T>(IObservable<T> source)
        {
            return Observable.Create<T>(observer =>
            {
                var d = source.Subscribe(observer);

                return Disposable.Create(() =>
                {
                     d.Dispose();
                });
            });
        }

        public void Dispose()
        {
            _queue.CompleteAdding();
            if (_worker != null)
            {
                _worker.Wait(TimeSpan.FromSeconds(30));
            }
        }
    }
}
