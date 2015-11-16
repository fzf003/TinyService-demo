using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyService
{
    public abstract class AbstractInterceptor : IInterceptor
    {

        public virtual void Intercept(IInvocation invocation)
        {
            invocation.Proceed();
        }
    }
}
