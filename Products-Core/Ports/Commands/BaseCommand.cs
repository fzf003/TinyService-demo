using Products_Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Command;
using TinyService.Domain.Entities;

namespace Products_Core.Ports.Commands
{
    public abstract class BaseCommand : ICommand
    {
        public BaseCommand()
        {
            this.Id = Guid.NewGuid().ToString("N");
            this.Timestamp = DateTime.Now;
        }
        public string Id
        {
            get;
            set;
        }

        public DateTime Timestamp
        {
            get;
            set;
        }



        public string AggregateRootId
        {
            get;
            set;
        }
    }

    public abstract class BaseCommand<TAggregateRoot> : ICommand<TAggregateRoot>
         where TAggregateRoot : IAggregateRoot
    {
        public BaseCommand()
        {
            this.Id = Guid.NewGuid().ToString("N");
            this.Timestamp = DateTime.Now;
        }
        public string Id
        {
            get;
            set;
        }

        public DateTime Timestamp
        {
            get;
            set;
        }
 
        public string AggregateRootId
        {
            get;
            set;
        }
    }

    public class TaskComand:BaseCommand
    {
        TaskCompletionSource<dynamic> tcs = new TaskCompletionSource<dynamic>();

        public Task<dynamic> GetResponse()
        {
            return tcs.Task;
        }
    }
}
