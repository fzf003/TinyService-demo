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
                    this._servicebus.Publish<AppMessage>(new AppMessage()
                    {
                        Id = "开始" + Guid.NewGuid().ToString(),
                        Timestamp = DateTime.Now
                    });

                    invocation.Proceed();
                    this._servicebus.Publish<AppMessage>(new AppMessage()
                    {
                        Id = "完成" + Guid.NewGuid().ToString(),
                        Timestamp = DateTime.Now
                    });
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                this._servicebus.PublishAsync<AppMessage>(new AppMessage()
                {
                    Id = "异常" + ex.Message,
                    Timestamp = DateTime.Now
                });
            }


        }
    }
}
