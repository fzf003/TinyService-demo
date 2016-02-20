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
    public class CommandResultProcessor : IDisposable
    {
        static  ConcurrentDictionary<string, CommandTaskCompletionSource> _commandTaskDict = new ConcurrentDictionary<string, CommandTaskCompletionSource>();
        private readonly BlockingCollection<CommandResult> _commandExecutedMessageLocalQueue;
        private readonly Worker _commandExecutedMessageWorker;
        public CommandResultProcessor()
        {
             this._commandExecutedMessageLocalQueue = new BlockingCollection<CommandResult>(new ConcurrentQueue<CommandResult>());
            this._commandExecutedMessageWorker = new Worker("CommandResult", () => ProcessMessage(this._commandExecutedMessageLocalQueue.Take()));
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

        public void SuccessCommand(ICommand command)
        {
            CommandTaskCompletionSource commandTaskCompletionSource;
            if (_commandTaskDict.TryRemove(command.GetHashCode().ToString(), out commandTaskCompletionSource))
            {
                var commandResult = new CommandResult { Status = CommandStatus.Success, CommandId = command.GetHashCode().ToString() };
                commandTaskCompletionSource.TaskCompletionSource.TrySetResult(commandResult);
            }
        }

        public void AddCommandResult(CommandResult result)
        {
            _commandExecutedMessageLocalQueue.Add(result);
        }

        void ProcessMessage(CommandResult result)
        {
            CommandTaskCompletionSource commandTaskCompletionSource;
            if (_commandTaskDict.TryGetValue(result.CommandId, out commandTaskCompletionSource))
            {
                if (result.Status == CommandStatus.Success)
                {
                  
                }

                if(result.Status==CommandStatus.Failed)
                {

                }
            }

            //this._commandTaskDict.GetOrAdd(result.CommandId, new CommandTaskCompletionSource() { TaskCompletionSource = new TaskCompletionSource<CommandResult>() });

            Console.WriteLine(string.Format("{0}-{1}", result.CommandId, result.Status));
        }

        public void Dispose()
        {
            //this._commandExecutedMessageWorker.Stop();
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

        public override string ToString()
        {
            return string.Format("[CommandId={0},Status={1},Result={2},ResultType={3}]",
                CommandId,
                Status,
                Result,
                ResultType);
        }
    }

    public enum CommandStatus
    {
        None = 0,
        Success = 1,
        NothingChanged = 2,
        Failed = 3
    }
}
