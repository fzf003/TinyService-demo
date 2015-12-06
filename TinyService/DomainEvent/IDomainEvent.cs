using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Infrastructure;
 namespace TinyService.DomainEvent
{
    public interface IDomainEvent : IMessage<string>
    {
 
    }

    
}
