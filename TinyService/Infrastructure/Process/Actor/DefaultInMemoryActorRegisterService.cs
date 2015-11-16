using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyService.Infrastructure.Process.Actor
{
    public interface IActorRegisterService
    {
       
        Uri ResolveActorLocation(Address address);

        void Register(Address address, Uri universalActorLocation);
        
    }

    public class DefaultInMemoryActorRegisterService : IActorRegisterService
    {
        private readonly ConcurrentDictionary<Address, Uri> _addresses = new ConcurrentDictionary<Address, Uri>();

    
        public Uri ResolveActorLocation(Address address)
        {
            return _addresses[address];
        }

        public void Register(Address address, Uri universalActorLocation)
        {
            if (TheAddressIsNotRegistered(address))
            {
                _addresses.TryAdd(address, universalActorLocation);
            }
        }

        private bool TheAddressIsNotRegistered(Address address)
        {
            return !_addresses.ContainsKey(address);
        }


       
    }
}
