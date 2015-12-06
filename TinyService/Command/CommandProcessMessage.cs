using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Domain.Entities;

namespace TinyService.Command
{
    /// <summary>
    /// 
    /// </summary>
    public class CommandProcessMessage
    {
        public ICommand Command { get; set; }

        //public string AggregateRoot { get; set; }

        public TaskCompletionSource<CommandExecuteResult> TaskCompletionSource { get; set; }
 
        public CommandProcessMessage()
        {
            this.TaskCompletionSource = new TaskCompletionSource<CommandExecuteResult>();
        }
    }
}
