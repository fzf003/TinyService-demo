using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TinyService.Infrastructure.Proxy
{
    public class InterceptorInvocation : AbstractInvocation
    {
        private readonly IInvocation _parent;

         public InterceptorInvocation(IInvocation parent, IInterceptor[] interceptors)
            : base(parent.Proxy, interceptors, parent.Method, parent.Arguments)
        {
            if (parent == null)
            {
                throw new ArgumentNullException("parent");
            }

            _parent = parent;
        }

         protected override void InvokeMethodOnTarget()
        {
            _parent.Method.Invoke(_parent.InvocationTarget, _parent.Arguments);
        }

         public override object InvocationTarget
        {
            get { return _parent.InvocationTarget; }
        }

         public override Type TargetType
        {
            get { return _parent.TargetType; }
        }
 
        public override MethodInfo MethodInvocationTarget
        {
            get { return _parent.MethodInvocationTarget; }
        }
    }
}
