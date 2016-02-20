using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.DomainEvent;

namespace TinyService.Command
{
    public class CommandProcessContext
    {
        public List<IDomainEvent> Events { get; set; }

        public ICommand Command { get; set; }
     }
}
