using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Infrastructure;

namespace TinyService.Command.Impl
{
    [Component(IsSingleton = true)]
    public class CommandResultProcessor 
    {
        ConcurrentDictionary<string, CommandTaskCompletionSource> _commandTaskDict = new ConcurrentDictionary<string, CommandTaskCompletionSource>();
        //private readonly BlockingCollection<CommandResult> _commandExecutedMessageLocalQueue;
        //private readonly Worker _commandExecutedMessageWorker;
        public CommandResultProcessor()
        {
             //this._commandExecutedMessageLocalQueue = new BlockingCollection<CommandResult>(new ConcurrentQueue<CommandResult>());
            //this._commandExecutedMessageWorker = new Worker("CommandResult", () => ProcessMessage(this._commandExecutedMessageLocalQueue.Take()));
            //this._commandExecutedMessageWorker.Start();
            //this._commandExecutedMessageLocalQueue.GetConsumingEnumerable().ToObservable(TaskPoolScheduler.Default).Subscribe(p => ProcessMessage(p));
        }

        public void RegisterProcessingCommand(ICommand command, TaskCompletionSource<CommandResult> taskCompletionSource)
        {
            if (!_commandTaskDict.TryAdd(command.GetHashCode().ToString(), new CommandTaskCompletionSource { TaskCompletionSource = taskCompletionSource }))
            {
                throw new Exception(string.Format("重复处理请求, type:{0}, id:{1}", command.GetType().Name, command.GetHashCode().ToString()));
            }
             
        }

        public void ProcessFailedSendingCommand(ICommand command,string errormsg)
        {
            CommandTaskCompletionSource commandTaskCompletionSource;
            if (_commandTaskDict.TryRemove(command.GetHashCode().ToString(), out commandTaskCompletionSource))
            {
                var commandResult = new CommandResult { Status = CommandStatus.Failed, CommandId = command.GetHashCode().ToString(), Result = errormsg };
                commandTaskCompletionSource.TaskCompletionSource.TrySetResult(commandResult);
            }
        }

        public void ProcessFailedSendingCommand(ICommand command, Exception exception)
        {
            CommandTaskCompletionSource commandTaskCompletionSource;
            if (_commandTaskDict.TryRemove(command.GetHashCode().ToString(), out commandTaskCompletionSource))
            {
                commandTaskCompletionSource.TaskCompletionSource.TrySetException(exception);
            }
        }

        public void SuccessCommand(ICommand command)
        {
            CommandTaskCompletionSource commandTaskCompletionSource;
            if (_commandTaskDict.TryRemove(command.GetHashCode().ToString(), out commandTaskCompletionSource))
            {
                var commandResult = new CommandResult { Status = CommandStatus.Success, CommandId = command.GetHashCode().ToString() };
                commandTaskCompletionSource.TaskCompletionSource.TrySetResult(commandResult);
            }
        }

        
    }

    class CommandTaskCompletionSource
    {
        public TaskCompletionSource<CommandResult> TaskCompletionSource { get; set; }
    }


    public class CommandResult
    {
        public CommandStatus Status { get; set; }

        public string CommandId { get; set; }

        public string Result { get; set; }

        public string ResultType { get; set; }

        public CommandResult() { }

       
    }

    public enum CommandStatus
    {
        None = 0,
        Success = 1,
        Failed = 2
    }
}
