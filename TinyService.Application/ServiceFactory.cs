using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Infrastructure;
using TinyService.Infrastructure.Proxy;
using TinyService.Extension.ServiceBus;
using TinyService.Service;

namespace TinyService.Application
{
    /*  public class ServiceFactory
      {
          public static T Create<T>(Type serviceType)
          {
             // if (client == null) throw new ArgumentNullException("client");
              if (!serviceType.IsInterface) throw new Exception("Only interface allowed.");

              var gen = new ProxyGenerator();
              var proxy = gen.CreateInterfaceProxyWithoutTarget(
                  serviceType,
                  Type.EmptyTypes,
                  new ServiceActionInterceptor());

              return (T)proxy;
          }
      }*/

    [Component(IsSingleton = false)]
    public class ServiceActionInterceptor : IInterceptor
    {
       private readonly IServiceBus _servicebus;
       public ServiceActionInterceptor(IServiceBus servicebus)
        {
            this._servicebus = servicebus;
        }

        public void Intercept(IInvocation invocation)
        {
          
            try
            {
                //invocation.ReturnValue = "sddssd";
                invocation.Proceed();
            }catch(Exception ex)
            {
              
            }
        }
    }

    [Interceptor(typeof(ServiceActionInterceptor))]
    public interface ITestClient
    {
        string GetName();
    }


     [Component(IsSingleton = false)]
    public class TestCLient : ITestClient
    {
        public string GetName()
        {
           
            return "a";
        }
    }
}
