using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyService.Command
{
    public interface ICommandHandler<TCommand>
         where TCommand : class,ICommand
    {
        void Handle(TCommand command);
    }
}
