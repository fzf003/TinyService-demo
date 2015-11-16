using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyService.MessageBus.Impl
{
    public class DefaultMessageBus : AbstractMessageBus
    {
        public DefaultMessageBus(MessageBusConfiguration busconfig)
            : base(busconfig)
        {
            
        }
     }
}
