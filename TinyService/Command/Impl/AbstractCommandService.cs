using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Domain.Entities;
using TinyService.DomainEvent;
using TinyService.Extension;
namespace TinyService.Command.Impl
{
    public abstract class AbstractCommandService : ICommandService
    {
        protected abstract ICommandAsyncHandler<TCommand> GetAsyncCommandHandler<TCommand>()
                            where TCommand : class,ICommand;

        protected abstract ICommandHandler<TCommand> GetCommandHandler<TCommand>()
                            where TCommand : class,ICommand;

        private readonly CommandExecuteResultProcess _commandprocess;

        //private readonly IDomainStore<string, IAggregateRoot> _domainrepository;

        public AbstractCommandService(CommandExecuteResultProcess commandprocess, IDomainStore<string, IAggregateRoot> domainrepository)
        {
            if (commandprocess == null)
            {
                throw new ArgumentNullException("");
            }

            this._commandprocess = commandprocess;
            this._commandprocess.Start();
            //this._domainrepository = domainrepository;
        }

        public CommandExecuteResult Send<TCommand>(TCommand command, Action<IEnumerable<IDomainEvent>> successcallback, int timeoutmilliseconds = 10000) where TCommand : class, ICommand
        {
             return SendAsync(command, successcallback, timeoutmilliseconds)
                  .GetAwaiter()
                  .GetResult();
        }

        public async Task<CommandExecuteResult> SendAsync<TCommand>(TCommand command, Action<IEnumerable<IDomainEvent>> successcallback, int timeoutmilliseconds = 10000) where TCommand : class, ICommand
        {
            var result = new CommandExecuteResult();
            try
            {
                var sendmessage = BuliderMessage(command);

                if (this._commandprocess.RegisterProcessingCommand(sendmessage))
                {
                   
                    var executeresult = await GetAsyncCommandHandler<TCommand>()
                       .HandleAsync(command)
                       .Timeout(timeoutmilliseconds);

                 
                    if (executeresult.Status == CommandExecuteStatus.Success)
                    {
                        var curraggregateroot = ((IAggregateRoot)executeresult.AggregateRoot);
                        var processcontext = new CommandProcessContext();

                        processcontext.Command = command;

                        processcontext.Events = curraggregateroot.GetUncommittedChanges().ToList();
 
                        PublishEvents(processcontext, successcallback);
                        curraggregateroot.MarkChangesAsCommitted();
                        return await sendmessage.TaskCompletionSource.Task;
                    }

                    this._commandprocess.ProcessFailedCommand(sendmessage);

                    result = new CommandExecuteResult(CommandExecuteStatus.Failed, command.Id, result.ErrorMessage);
                }
                else
                {
                    result = new CommandExecuteResult(CommandExecuteStatus.Failed, command.Id,  "命令处理器注册失败");
                }
            }
            catch (Exception ex)
            {
                result = new CommandExecuteResult(CommandExecuteStatus.Failed, command.Id,  ex.Message);
            }
            return result;
        }
        void PublishEvents(CommandProcessContext ctx, Action<IEnumerable<IDomainEvent>> successcallback)
        {
            if (successcallback != null)
            {
                successcallback(ctx.Events);
            }
        }


        CommandProcessMessage BuliderMessage(ICommand command)
        {
            return new CommandProcessMessage()
            {
                Command = command,
                TaskCompletionSource = new TaskCompletionSource<CommandExecuteResult>()
            };
        }

        public virtual void Dispose()
        {
            if (this._commandprocess != null)
            {
                this._commandprocess.Dispose();
            }
        }
 
    }
}
