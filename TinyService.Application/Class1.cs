using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace TinyService.Application
{
    public class RxPublisher<T>
    {
        private const int BoundedCapacity = 1024;
        private readonly BlockingCollection<T> _queue;
        private Task _worker;
        private bool Started = false;
        private ReplaySubject<T> _replaysubjects;
        public RxPublisher()
        {
            _queue = new BlockingCollection<T>(new ConcurrentQueue<T>(), BoundedCapacity);
            this._replaysubjects = new ReplaySubject<T>();
        }

        public void Start()
        {
            _worker = Task.Factory.StartNew(Working);
            Started = true;
        }

        public bool SendMessage(T message)
        {
            return this._queue.TryAdd(message);
        }
        
        private void Working()
        {
            foreach (var @event in _queue.GetConsumingEnumerable())
            {
                this._replaysubjects.OnNext(@event);
            }
        }



        public IObservable<T> GetDomainEvents()
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
