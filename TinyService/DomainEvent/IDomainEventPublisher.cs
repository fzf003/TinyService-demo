using System;
namespace TinyService.DomainEvent
{
    public interface IDomainEventPublisher:IDisposable
    {
        bool Dispatch(IDomainEvent @event);
        void Start();
        IObservable<IDomainEvent> GetDomainEvents();
    }
}
