using Castle.DynamicProxy;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyService.Infrastructure.Proxy
{
    public interface IInterceptorProxy : IInterceptor
    {
        
        IServiceLocator Container { get; set; }
    }
}
