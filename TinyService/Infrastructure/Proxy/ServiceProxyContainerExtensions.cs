using Castle.DynamicProxy;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyService.Infrastructure.Proxy
{
    public static class ServiceProxyContainerExtensions
    {
        static ConcurrentDictionary<string, object> dic = new ConcurrentDictionary<string, object>();
        public static void InitInterceptor(this IServiceLocator serviceContainer)
        {
            if (serviceContainer == null)
            {
                throw new ArgumentNullException("serviceContainer");
            }

            ServiceLocator.SetLocatorProvider(() => serviceContainer);
        }

        public static T CreateProxy<T>(this T objectservice, params IInterceptor[] interceptor)
        {
            object result = default(T);
            if (dic.TryGetValue(typeof(T).FullName, out result))
            {
                return (T)result;
            }
            else
            {

                var interceptorProxy = new InterceptorProxy { Container = ServiceLocator.Current };
                IProxyFactory _proxyFactory = ServiceLocator.Current.GetInstance<IProxyFactory>();
                //
                var proxy = _proxyFactory.CreateProxy(ServiceLocator.Current.GetInstance<T>(), interceptorProxy);
                     
                result = proxy;
                dic.TryAdd(typeof(T).FullName, proxy);
            }

            return (T)result;

        }
    }
}
