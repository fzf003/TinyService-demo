using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Domain.Entities;
using TinyService.DomainEvent;

namespace TinyService.Command
{
    public interface ICommandService:IDisposable
    {
        CommandExecuteResult Send<TCommand>(TCommand command, Action<IEnumerable<IDomainEvent>> successcallback, int timeoutmilliseconds = 10000)
            where TCommand : class, ICommand;


        Task<CommandExecuteResult> SendAsync<TCommand>(TCommand command, Action<IEnumerable<IDomainEvent>> successcallback, int timeoutmilliseconds = 10000)
            where TCommand : class, ICommand;
           
    }
}
