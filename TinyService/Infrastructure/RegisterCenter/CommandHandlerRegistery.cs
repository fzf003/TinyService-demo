using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Command;

namespace TinyService.Infrastructure.RegisterCenter
{
    public class CommandHandlerRegistery
    {
        ConcurrentDictionary<Type, CommandHandlerDesc> CommandExecutionDetailsMap = new ConcurrentDictionary<Type, CommandHandlerDesc>();

       static Lazy<CommandHandlerRegistery> _instance = new Lazy<CommandHandlerRegistery>(() => new CommandHandlerRegistery());

       public static CommandHandlerRegistery Instance
       {
           get
           {
               return _instance.Value;
           }
       }
       public class CommandHandlerDesc
       {
           public CommandHandlerDesc(dynamic handler, Type commandtype, bool isasync)
           {
               this.Handler = handler;
               this.CommandType = commandtype;
               this.IsAsync = isasync;
           }
           public dynamic Handler { get; private set; }
           public bool IsAsync { get; private set; }

           public Type CommandType { get; private set; }
       }

       public void RegisterCommandHandler(ICommand command, dynamic commandhandler,bool isasync=true)
       {
            CommandExecutionDetailsMap.GetOrAdd(command.GetType(), (type) =>
           {
               return new CommandHandlerDesc(commandhandler, type, isasync);
           });
       }

      public CommandHandlerDesc GetCommandHandler(ICommand command)
       {
          CommandHandlerDesc handler=null;
          CommandExecutionDetailsMap.TryGetValue(command.GetType(), out handler);
          return handler;
       }
    }
}
