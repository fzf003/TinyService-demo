using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Infrastructure;

namespace TinyService.Command.Impl
{
    [Component(IsSingleton = true)]
    public class CommandDispenser : MessageDispenser<ICommand>, ICommandDispenser
    {

    }

     
}
