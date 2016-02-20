using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Infrastructure;
using TinyService;
using TinyService.Extension.ServiceBus;
using TinyService.MessageBus.Contract;
using TinyService.Service;
namespace TinyService.Application
{
    [Component]
    public class AuthorizationInterceptor : AbstractInterceptor
    {
        private readonly IServiceBus _servicebus;
        public AuthorizationInterceptor(IServiceBus servicebus)
        {
            this._servicebus = servicebus;
        }

        public override void Intercept(IInvocation invocation)
        {
            try
            {
                //if (invocation.ReturnValue.GetType() == typeof(string))
                {
                     

                    invocation.Proceed();
                
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                
            }


        }
    }
}
