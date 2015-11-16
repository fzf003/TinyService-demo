using Castle.DynamicProxy;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Infrastructure;

namespace TinyService.Infrastructure.Proxy
{
    [Component]
    public class InterceptorProxy : IInterceptorProxy
    {
        public IServiceLocator Container { get; set; }

        private List<IInterceptor> store = new List<IInterceptor>(); 
 
        public void Intercept(IInvocation invocation)
        {
            if (invocation == null)
            {
                throw new ArgumentNullException("invocation");
            }

            var typeInterceptorAttributes = InterceptorHelper.CollectTypeInterceptors(invocation.TargetType);
            var methodInterceptorAttributes = InterceptorHelper.CollectMethodInterceptors(invocation.Method, invocation.MethodInvocationTarget).Distinct();
            if (typeInterceptorAttributes.Any() || methodInterceptorAttributes.Any())
            {
                var typeInterceptors = typeInterceptorAttributes.OrderBy(SortOrder).Select(CreateInterceptor).Where(InterceptorExists);
                var methodInterceptors = methodInterceptorAttributes.OrderBy(SortOrder).Select(CreateInterceptor).Where(InterceptorExists);
                var interceptors = typeInterceptors.Union(methodInterceptors);
                //var _parent = invocation;
                //store.AddRange(interceptors);
                //     var result = _parent.Method.Invoke(_parent.InvocationTarget, _parent.Arguments);
                //_parent.ReturnValue = result;
                var interceptorInvocation = new InterceptorInvocation(invocation, interceptors.ToArray());
                //Console.WriteLine("cc:" + interceptorInvocation.ReturnValue);
                //interceptorInvocation.ReturnValue = "pp";
                interceptorInvocation.Proceed();
                //_parent.Proceed();
                 
            }
            else
            {
                invocation.Proceed();
            }
        }

        private static int SortOrder(InterceptorAttribute a)
        {
            return a.Order;
        }

        private IInterceptor CreateInterceptor(InterceptorAttribute interceptorAttribute)
        {
            if (String.IsNullOrEmpty(interceptorAttribute.InterceptorName))
            {
                return Container.GetInstance(interceptorAttribute.InterceptorType) as IInterceptor;
            }

            return Container.GetInstance(interceptorAttribute.InterceptorType, interceptorAttribute.InterceptorName) as IInterceptor;
        }

        private bool InterceptorExists(IInterceptor interceptor)
        {
            return interceptor != null;
        }
    

public List<IInterceptor> Interceptorstore
{
    get { return store; }
}
}
}
