using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Domain.Entities;
using TinyService.Infrastructure;

namespace TinyService.Command.Impl
{
    [Component(IsSingleton=true)]
    public class DefaultCommandService : AbstractCommandService
    {
        public DefaultCommandService(IDomainStore<string, IAggregateRoot> domainrepository)
            : base(new CommandExecuteResultProcess(),domainrepository)
        {

        }
        public DefaultCommandService(CommandExecuteResultProcess commandexecuteprocess, IDomainStore<string, IAggregateRoot> domainrepository)
            : base(commandexecuteprocess, domainrepository)
        {
             
        }

        protected override ICommandHandler<TCommand> GetCommandHandler<TCommand>()
        {
            var handler = ObjectFactory.GetService<ICommandHandler<TCommand>>();
            if (handler == null)
            {
                throw new Exception("handler 没有找到");
            }
            return handler;
        }

        protected override ICommandAsyncHandler<TCommand> GetAsyncCommandHandler<TCommand>()
        {
            var handler = ObjectFactory.GetService<ICommandAsyncHandler<TCommand>>();
            if (handler == null)
            {
                throw new Exception("handler 没有找到");
            }
            return handler;
        }

        
        
    }
}
