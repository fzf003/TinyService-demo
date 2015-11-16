using Castle.DynamicProxy;
using System;
namespace TinyService.Infrastructure.Proxy
{
    public interface IProxyFactory
    {
        object CreateProxy(object target, IInterceptorProxy interceptorProxy);
        object CreateProxy(object target, params IInterceptor[] interceptor);
    }
}
