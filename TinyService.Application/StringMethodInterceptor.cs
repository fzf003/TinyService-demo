using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Infrastructure.CommonComposition;

namespace TinyService.Application
{
    [Component]
    public class StringMethodInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine("开始:" + invocation.Method.Name + "===============================");
            Console.WriteLine("开始:" + invocation.ReturnValue + "===============================");

            //if (invocation.Method.ReturnType == typeof(string))
            //{
            //    invocation.ReturnValue = "intercepted-" + invocation.Method.Name;

            //}
            //else
            //{
            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ex:" + ex.Message);
            }
            // }
            Console.WriteLine("结束" + invocation.Method.Name + "===============================");
        }
    }
}
