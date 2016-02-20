using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Infrastructure;
using TinyService.Service;

namespace TinyService.Application.Services
{
    public abstract class ServiceBase 
    {
        public abstract T ResolveService<T>();
 
        public IServiceBus ServiceBus
        {
            get
            {
                return this.ResolveService<IServiceBus>();
            }
        }



    }
}
