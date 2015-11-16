using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyService.Infrastructure.Proxy
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class InterceptorAttribute : Attribute
    {

        public InterceptorAttribute(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (!typeof(IInterceptor).IsAssignableFrom(type))
            {
                throw new ArgumentOutOfRangeException("type", type, "该类型没有实现 Castle.DynamicProxy.IInterceptor");
            }

            InterceptorType = type;
            Order = Int32.MaxValue;
        }


        public string InterceptorName { get; set; }

        public Type InterceptorType { get; private set; }

        public int Order { get; set; }

     
    }
}
