using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using TinyIoC;

namespace TinyService.TinyIoc
{
    public class TinyIoCServiceLocator : ServiceLocatorImplBase
    {

        private readonly TinyIoCContainer _container;


        [SecuritySafeCritical]
        protected TinyIoCServiceLocator()
        {

        }


        [SecuritySafeCritical]
        public TinyIoCServiceLocator(TinyIoCContainer container)
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

            return key != null ? _container.Resolve(serviceType, key) : _container.Resolve(serviceType);
        }


        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException("serviceType");
            }
            var enumerableType = typeof(IEnumerable<>).MakeGenericType(serviceType);
            IEnumerable<object> instance = _container.ResolveAll(enumerableType);
            return instance;
        }
    }
}
