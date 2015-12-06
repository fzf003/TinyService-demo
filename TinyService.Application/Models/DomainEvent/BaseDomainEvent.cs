using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.DomainEvent;

namespace TinyService.Application.Models.DomainEvent
{
    public class BaseDomainEvent : IDomainEvent
    {
        public BaseDomainEvent()
        {
            this.Id = Guid.NewGuid().ToString("N");
            this.Timestamp = DateTime.Now;
        }
        public int Version { get; set; }
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
    }
}
