using Autofac;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace TinyService.autofac
{
    public class AutofacServiceLocator : ServiceLocatorImplBase
    {

        private readonly IComponentContext _container;


        [SecuritySafeCritical]
        protected AutofacServiceLocator()
        {

        }


        [SecuritySafeCritical]
        public AutofacServiceLocator(IComponentContext container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            _container = container;
        }


        protected override object DoGetInstance(Type serviceType, string key)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException("serviceType");
            }
            return key != null ? _container.ResolveNamed(key, serviceType) : _container.Resolve(serviceType);
        }


        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException("serviceType");
            }
            var enumerableType = typeof(IEnumerable<>).MakeGenericType(serviceType);

            object instance = _container.Resolve(enumerableType);
            return ((IEnumerable)instance).Cast<object>();
        }
    }
}
