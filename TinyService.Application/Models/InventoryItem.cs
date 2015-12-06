using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Application.Models.DomainEvent;
using TinyService.Domain.Entities;
using TinyService.Infrastructure;

namespace TinyService.Application.Models
{
    public class InventoryItem : AggregateRoot<string>
    {
        private bool _activated;
        private string _id;

        private string _itemName;
        private int _count;

        public InventoryItem()
        {

        }

        protected void Apply(InventoryItemRenamed @event)
        {
            Console.WriteLine("重命名为:{0}", @event.NewName);
            this._itemName = @event.NewName;

        }
        protected void Apply(InventoryItemCreated e)
        {
            _id = e.Id;
            _activated = true;
            _itemName = e.Name;

            Console.WriteLine("新建:{0}", e.Name);
        }

        protected void Apply(InventoryItemDeactivated e)
        {
            _activated = false;

            Console.WriteLine("取消激活:{0}", e.Id);
        }


        protected void Apply(ItemsCheckedInToInventory @event)
        {
            Console.WriteLine("CheckIn:{0}--{1}", @event.Id, @event.Count);
            this._count = @event.Count;
        }


        public void ChangeName(string newName)
        {
            ApplyChange(new InventoryItemRenamed(_id, newName));
        }

        public void Remove(int count)
        {
            if (count <= 0) throw new InvalidOperationException("cant remove negative count from inventory");
            ApplyChange(new ItemsRemovedFromInventory(_id, count));
        }


        public void CheckIn(int count)
        {
            if (count <= 0) throw new InvalidOperationException("must have a count greater than 0 to add to inventory");

            ApplyChange(new ItemsCheckedInToInventory(_id, count));
        }

        public void Deactivate()
        {
            //if (!_activated) throw new InvalidOperationException("already deactivated");
            ApplyChange(new InventoryItemDeactivated(_id));
        }



        public override string Id
        {
            get { return _id; }
        }

        public string Name
        {
            get
            {
                return this._itemName;
            }
        }


        public InventoryItem(string id, string name)
        {
            ApplyChange(new InventoryItemCreated(id, name));
        }
    }

}
