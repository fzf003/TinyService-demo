using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

namespace TinyService.Infrastructure.Process.Actor
{
    public class ActorObservable<T> : IObservable<T>
    {
        private readonly ActorApplication _actorRef;

        public ActorObservable(ActorApplication actorRef)
        {
            _actorRef = actorRef;
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
           



            return Disposable.Create(() => {});
        }
    }
}
