using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyService.Command
{
    public interface ICommandAsyncHandler<TCommand>
        where TCommand : class,ICommand
        
    {
        Task<CommandExecuteResult> HandleAsync(TCommand command);
    }
}
