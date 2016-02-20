using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyService.Command
{
    public interface ICommandProcessor : IDisposable
    {

        void ProcessCommand<TCommand>(TCommand command)
             where TCommand : class,ICommand;
        
    }
}
