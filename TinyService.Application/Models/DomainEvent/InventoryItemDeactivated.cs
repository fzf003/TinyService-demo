using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyService.Application.Models.DomainEvent
{
    public class InventoryItemDeactivated : BaseDomainEvent
    {
       

        public InventoryItemDeactivated(string id)
        {
            Id = id;
        }
    }

    public class InventoryItemCreated : BaseDomainEvent
    {
        public readonly string Name;
        public InventoryItemCreated(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public class InventoryItemRenamed : BaseDomainEvent
    {
 
        public readonly string NewName;

        public InventoryItemRenamed(string id, string newName)
        {
            Id = id;
            NewName = newName;
        }
    }

    public class ItemsCheckedInToInventory : BaseDomainEvent
    {
        
        public readonly int Count;

        public ItemsCheckedInToInventory(string id, int count)
        {
            Id = id;
            Count = count;
        }
    }

    public class ItemsRemovedFromInventory : BaseDomainEvent
    {
         
        public readonly int Count;

        public ItemsRemovedFromInventory(string id, int count)
        {
            Id = id;
            Count = count;
        }
    }
}
