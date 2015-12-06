using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Command;
using TinyService.Domain.Entities;
using TinyService.Domain.Repository;
using TinyService.Infrastructure;

namespace TinyService.Application.Models.Command
{
    public class ReNameCommand : BaseCommand
    {
        public string NewName { get; set; }
    }

    public class CreateItemCommand : BaseCommand
    {
        public string Name { get; set; }

    }

    [Component(IsSingleton = true)]
    public class MyInventoryHandler : ICommandAsyncHandler<CreateItemCommand>
    {
        private readonly IRepository<string, InventoryItem> _domainrepository;
        public MyInventoryHandler(IRepository<string, InventoryItem> domainrepository)
        {
            this._domainrepository = domainrepository;
        }

        public async Task<CommandExecuteResult> HandleAsync(CreateItemCommand command)
        {
            var item = new InventoryItem(command.Id, command.Name);

            _domainrepository.Insert(item);

            return new CommandExecuteResult()
            {
                Status = CommandExecuteStatus.Success,
                //AggregateRootId = item.Id,
                AggregateRoot=item,
                ErrorMessage = "成功"
            };
        }
    }

    [Component(IsSingleton = true)]
    public class InventoryCommandHandler : ICommandAsyncHandler<ReNameCommand>
    {
        private readonly IRepository<string, InventoryItem> _domainrepository;
        public InventoryCommandHandler(IRepository<string, InventoryItem> domainrepository)
        {
            this._domainrepository = domainrepository;
        }
        public Task<CommandExecuteResult> HandleAsync(ReNameCommand command)
        {
            var item = this._domainrepository.Get(command.AggregateRootId);

            item.ChangeName(command.NewName);

            this._domainrepository.InsertOrUpdate(item);

            return Task.FromResult(new CommandExecuteResult()
            {
                Status = CommandExecuteStatus.Success,
                //AggregateRootId = item.Id,
                AggregateRoot=item,
                ErrorMessage = "成功"
             });
        }
    }


}

