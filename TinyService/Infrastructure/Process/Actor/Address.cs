using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyService.Infrastructure.Process.Actor
{
    public class Address
    {
      
        public Address(string actorName)
        {
            ActorName = actorName;
        }

        public string ActorName { get; private set; }

        private bool Equals(Address other)
        {
            return Equals(ActorName, other.ActorName);
        }

       
        public override int GetHashCode()
        {
            return (ActorName != null ? ActorName.GetHashCode() : 0);
        }

         
        public override bool Equals(object obj)
        {
            return Equals((Address)obj);
        }

       
        public override string ToString()
        {
            return ActorName;
        }
    }
}
