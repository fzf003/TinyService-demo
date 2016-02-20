using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyService.Command
{
    public interface ICommandMapper
    {
        ICommandHandler<TCommand> GetCommandAction<TCommand>() where TCommand : class,ICommand;
    }
}
