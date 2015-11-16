using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Infrastructure.Message;

namespace TinyService.MessageBus.Contract
{

    public class AppMessage : Message
    {
        public AppMessage(string messageid)
        {
            this.messageId = messageid;
            this.datetime = DateTime.Now;
        }

        public string Name { get; set; }

        public AppMessage()
            : this(Guid.NewGuid().ToString("N"))
        {

        }
        public string messageId { get; set; }

        public DateTime datetime { get; set; }
    }
}
