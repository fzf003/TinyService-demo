using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Command;
using TinyService.Domain.Entities;
using TinyService.Domain.Repository;
using TinyService.Infrastructure;
using TinyService.Extension.Repository;
using TinyService.DomainEvent;

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
		private readonly IDomainEventPublisher _eventpublisher;
		public MyInventoryHandler(IRepository<string, InventoryItem> domainrepository, IDomainEventPublisher eventpublisher)
		{
			this._domainrepository = domainrepository;
			this._eventpublisher = eventpublisher;
		}

		public async Task<CommandExecuteResult> HandleAsync(CreateItemCommand command)
		{
			var item = new InventoryItem(command.Id, command.Name);

            await this._domainrepository.InsertAsync(item);

			return new CommandExecuteResult()
			{
				Status = CommandExecuteStatus.Success,
				AggregateRootId = item.Id,
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
		public async Task<CommandExecuteResult> HandleAsync(ReNameCommand command)
		{
			var item = this._domainrepository.Get(command.AggregateRootId);

			item.ChangeName(command.NewName);

			await this._domainrepository.InsertOrUpdateAsync(item);

            return new CommandExecuteResult()
            {
                Status = CommandExecuteStatus.Success,
                AggregateRootId = item.Id,
                AggregateRoot = item,
                ErrorMessage = "成功"
            };
		}
	}


}

