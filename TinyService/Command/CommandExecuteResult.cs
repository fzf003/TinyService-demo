using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using TinyService.DomainEvent;
namespace TinyService.Command
{

    public class CommandResponse
    {
        public CommandExecuteStatus Status { get; set; }
        public string ErrorMessage { get; set; }

    }

    [Serializable]
    public class CommandExecuteResult
    {
        public CommandExecuteStatus Status { get; set; }

        public string CommandId { get; set; }

        public string ErrorMessage { get; set; }

    
        public CommandExecuteResult()
        {

        }
       

        public CommandExecuteResult(CommandExecuteStatus status, string aggregaterootid, object aggregateroot ,  string errorMessage)
        {
            this.Status = status;
            
            this.ErrorMessage = errorMessage;
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
