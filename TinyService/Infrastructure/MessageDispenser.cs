using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Threading.Tasks;
using System.Reactive.Linq;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
namespace TinyService.Infrastructure
{
    public interface IMessageDispenser<T> : IDisposable
    {
        Task<bool> SendAsync(T command);

        IObservable<T> GetMessages();
    }

    public class MessageDispenser<T> :  IMessageDispenser<T>
    {
        private bool disposed;

        private IDisposable readSubscription;

        private BlockingCollection<T> received = new BlockingCollection<T>();

        private IObservable<T> receiver;

        private ReplaySubject<T> _replaysubjects;

        public MessageDispenser()
        {
            receiver = received.GetConsumingEnumerable().ToObservable(TaskPoolScheduler.Default);

            this._replaysubjects = new ReplaySubject<T>();

            receiver.Subscribe(this._replaysubjects);
        }

        public IObservable<T> GetMessages()
        {
            var src = _replaysubjects.ObserveOn(TaskPoolScheduler.Default);

            return ToMessage(src);
        }



        IObservable<T> ToMessage<T>(IObservable<T> source)
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

        //public event EventHandler Connected = (sender, args) => { };


        public event EventHandler Disconnected = (sender, args) => { };


        public event EventHandler Disposed = (sender, args) => { };

        protected void Disconnect()
        {
            Disconnect(false);
        }

        protected void Disconnect(bool disposing)
        {
            if (disposed && !disposing)
                throw new ObjectDisposedException(this.ToString());

            if (readSubscription != null)
            {
                readSubscription.Dispose();
            }

            readSubscription = null;

            Disconnected(this, EventArgs.Empty);
        }

        public void Dispose()
        {
            if (disposed)
                return;

            disposed = true;

            Disconnect(true);
            received.CompleteAdding();

            Disposed(this, EventArgs.Empty);
        }

        public Task<bool> SendAsync(T command)
        {
            return Task.FromResult(received.TryAdd(command));
        }
    }
}
