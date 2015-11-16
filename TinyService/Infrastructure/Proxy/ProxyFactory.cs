using Castle.DynamicProxy;
 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Infrastructure;

namespace TinyService.Infrastructure.Proxy
{
    [Component(IsSingleton = true)]
    public class ProxyFactory : IProxyFactory
    {
        private readonly ProxyGenerator _proxyGenerator;

        public ProxyFactory(ProxyGenerator proxyGenerator)
        {
            _proxyGenerator = proxyGenerator;
        }

        public ProxyFactory()
        {
            this._proxyGenerator = new ProxyGenerator();
        }


        public object CreateProxy(object target, IInterceptorProxy interceptorProxy)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }
            var targetType = target.GetType();
            var targetInterfaces = targetType.GetInterfaces();
            if (targetInterfaces.Any())
            {
                var proxy = _proxyGenerator.CreateInterfaceProxyWithTargetInterface(targetInterfaces.Last(), target, interceptorProxy);
                return proxy;
            }
            else
            {
                var greediestCtor = targetType.GetConstructors().OrderBy(x => x.GetParameters().Count()).LastOrDefault();
                var ctorDummyArgs = greediestCtor == null ? new object[0] : new object[greediestCtor.GetParameters().Count()];
                var proxy = _proxyGenerator.CreateClassProxyWithTarget(targetType, target, ctorDummyArgs, interceptorProxy);
                return proxy;
            }

            //if (targetInterfaces.Any())
            //{
            //    var proxy = _proxyGenerator.CreateInterfaceProxyWithTargetInterface(targetInterfaces.Last(), target, interceptorProxy);
            //    return proxy;
            //}
            //else
            //{
            //    var greediestCtor = targetType.GetConstructors().OrderBy(x => x.GetParameters().Count()).LastOrDefault();
            //    var ctorDummyArgs = greediestCtor == null ? new object[0] : new object[greediestCtor.GetParameters().Count()];
            //    var proxy = _proxyGenerator.CreateClassProxyWithTarget(targetType, target, ctorDummyArgs, interceptorProxy);
            //    return proxy;
            //}
        }


        public object CreateProxy(object target, params IInterceptor[] interceptor)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }
            var targetType = target.GetType();
            var targetInterfaces = targetType.GetInterfaces();
            if (targetInterfaces.Any())
            {
                var proxy = _proxyGenerator.CreateInterfaceProxyWithTargetInterface(targetInterfaces.Last(), target, interceptor);
                return proxy;
            }
            else
            {
                var greediestCtor = targetType.GetConstructors().OrderBy(x => x.GetParameters().Count()).LastOrDefault();
                var ctorDummyArgs = greediestCtor == null ? new object[0] : new object[greediestCtor.GetParameters().Count()];
                var proxy = _proxyGenerator.CreateClassProxyWithTarget(targetType, target, ctorDummyArgs, interceptor);
                return proxy;
            }
        }
    }
}
