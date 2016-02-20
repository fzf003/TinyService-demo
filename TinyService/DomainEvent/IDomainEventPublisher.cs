using System;
using System.Threading.Tasks;
using TinyService.Infrastructure;
namespace TinyService.DomainEvent
{
    public interface IEventPublisher : IDisposable, IMessageDispenser<IDomainEvent>
    {
        Task<bool> Dispatch(IDomainEvent @event);
    }
}
