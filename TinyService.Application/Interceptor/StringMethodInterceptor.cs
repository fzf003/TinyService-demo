using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Infrastructure;
using TinyService;
using TinyService.Infrastructure.Proxy;

namespace TinyService.Application
{
    [Component]
    public class StringMethodInterceptor : AbstractInterceptor
    {
        public override void Intercept(IInvocation invocation)
        {
            Console.WriteLine("开始:" + invocation.Method.Name + "===============================");

             invocation.Proceed();
 
            Console.WriteLine("结束" + invocation.Method.Name + "===============================");

        }


    }



}
