using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Infrastructure.RequestHandler;

namespace TinyService.Command.Impl
{
    public interface ICommandExecuteResultProcess : IDisposable
    {
        void Start();

        bool RegisterProcessingCommand(CommandProcessMessage message);

        void ProcessSuccessCommand(CommandProcessMessage message);

        void ProcessFailedCommand(CommandProcessMessage message);

    }
    public class CommandExecuteResultProcess : ICommandExecuteResultProcess
    {
        private const int BoundedCapacity = 1024;
        private readonly BlockingCollection<CommandProcessMessage> _queue;
        private Task _worker;
        private bool Started = false;
        private readonly ConcurrentDictionary<CommandProcessMessage, CommandTaskCompletionSource> _commandTaskDict;
        public CommandExecuteResultProcess()
        {
            _queue = new BlockingCollection<CommandProcessMessage>(new ConcurrentQueue<CommandProcessMessage>(), BoundedCapacity);
            _commandTaskDict = new ConcurrentDictionary<CommandProcessMessage, CommandTaskCompletionSource>();
        }

        public void Start()
        {
            _worker = Task.Factory.StartNew(Working);
            Started = true;
        }

        public bool RegisterProcessingCommand(CommandProcessMessage message)
        {
            if (!Started)
            {
                throw new InvalidOperationException("Not Start");
            }

            _queue.Add(message);

            return _commandTaskDict.TryAdd(message, new CommandTaskCompletionSource { TaskCompletionSource = message.TaskCompletionSource });
        }

        private void Working()
        {
            foreach (var message in _queue.GetConsumingEnumerable())
            {
                try
                {
                     ProcessSuccessCommand(message);
                }
                catch (Exception ex)
                {
                    ProcessFailedCommand(message);
                }
            }
        }

        public void ProcessSuccessCommand(CommandProcessMessage message)
        {
            CommandTaskCompletionSource commandTaskCompletionSource;
            if (_commandTaskDict.TryGetValue(message, out commandTaskCompletionSource))
            {
                var executeresult = new CommandExecuteResult(CommandExecuteStatus.Success,
                                        message.Command.Id, 
                                  
                                        string.Format("处理Command:{0}成功",
                                        message.Command.Id));
                commandTaskCompletionSource.TaskCompletionSource.TrySetResult(executeresult);
            }
        }

        public void ProcessFailedCommand(CommandProcessMessage message)
        {
            CommandTaskCompletionSource commandTaskCompletionSource;
            if (_commandTaskDict.TryRemove(message, out commandTaskCompletionSource))
            {
               var executeresult= new CommandExecuteResult(CommandExecuteStatus.Failed,
                                       message.Command.Id, 
                                   
                                       string.Format("处理Command:{0}失败",
                                       message.Command.Id));
                 commandTaskCompletionSource.TaskCompletionSource.TrySetResult(executeresult);
            }
        }

        public void Dispose()
        {
            _queue.CompleteAdding();
            if (_worker != null)
            {
                _worker.Wait(TimeSpan.FromSeconds(30));
            }
        }
    }

    class CommandTaskCompletionSource
    {
        public TaskCompletionSource<CommandExecuteResult> TaskCompletionSource { get; set; }
    }
}
