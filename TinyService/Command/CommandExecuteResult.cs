using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyService.Command
{
    [Serializable]
    public class CommandExecuteResult
    {
        
        public CommandExecuteStatus Status { get;  set; }
      
        public string CommandId { get;  set; }
    
         public string ErrorMessage { get;  set; }

         public string AggregateRootId { get; set; }

         public object AggregateRoot { get; set; }

       
        public CommandExecuteResult()
         {

         }

        public CommandExecuteResult(CommandExecuteStatus status, string commandId,  string errorMessage)
        {
            Status = status;
            CommandId = commandId;
             ErrorMessage = errorMessage;
        }
 
    }

    public enum CommandExecuteStatus
    {
        None = 0,
        Success = 1,
        Failed = 2
    }
}
