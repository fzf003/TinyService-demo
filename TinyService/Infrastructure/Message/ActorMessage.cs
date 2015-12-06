using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Infrastructure.Process.Actor;

namespace TinyService.Infrastructure
{
   
    public class ActorMessage : Message
    {

        public ActorMessage()
             :base()
        {
        }

        public Address To { get; set; }

        public void SetAddress(Address address)
        {
            this.To = address;
        }
     }
}
