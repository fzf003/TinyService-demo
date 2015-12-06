using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Application.Models.DomainEvent;
using TinyService.Command;
using TinyService.DomainEvent;
using TinyService.Infrastructure;

namespace TinyService.Application.Models
{
    public class BaseCommand:ICommand
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
    public class TestCommand : BaseCommand
    {
        public string Name { get; set; }
    }

    public class HelloCommand : BaseCommand
    {
        public string Name { get; set; }
    }

    public class InventoryItemCreatedCommand
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
   
    public class TestDomainEvent : BaseDomainEvent
    {
        public string Name { get; set; }
       
    }

    public class MyDomainEvent : BaseDomainEvent
    {
        public string My { get; set; }
    }

    public class InventoryCommandHandler
    {
        public void Handle(InventoryItemCreatedCommand command)
        {
            Console.WriteLine(command.Id+"|"+command.Name);
        }
    }

    [Component]
    public class TestCommandHandler:ICommandAsyncHandler<TestCommand>,
                                    ICommandAsyncHandler<HelloCommand>
    {

        public Task<CommandExecuteResult> HandleAsync(TestCommand command)
        {
            return Task.FromResult(new CommandExecuteResult(CommandExecuteStatus.Success, command.Id, "成功"));
        }

        public Task<CommandExecuteResult> HandleAsync(HelloCommand command)
        {
            return Task.FromResult(new CommandExecuteResult(CommandExecuteStatus.Success, command.Id,  "成功"));
        }
    }


}
