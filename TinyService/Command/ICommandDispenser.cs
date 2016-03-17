using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Infrastructure;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using TinyService.Command.Impl;
namespace TinyService.Command
{
    public interface ICommandDispenser : IMessageDispenser<ICommand>
    {

    }



    public static class CommandDispenserExtension
    {
        public static IObservable<Command.ICommand> PublishCommand(this ICommandDispenser dispenser)
        {
            return dispenser.GetMessages();
        }

        public static IDisposable RegisterCommand<TCommand>(this IObservable<Command.ICommand> dispenser, ICommandProcessor commandProcessor) where TCommand : class,ICommand
        {
            return dispenser.OfType<TCommand>()
                            .Do(command => Execute(command, commandProcessor, ObjectFactory.GetService<CommandResultProcessor>()))
                            .Subscribe();
        }

        static void Execute<TCommand>(TCommand command, ICommandProcessor commandProcessor, CommandResultProcessor commandResultProcessor) where TCommand : class,ICommand
        {
            try
            {
                commandProcessor.ProcessCommand<TCommand>(command);
                commandResultProcessor.SuccessCommand(command);
            }
            catch (Exception ex)
            {
                commandResultProcessor.ProcessFailedSendingCommand(command, ex);
            }
        }
    }
 
}
