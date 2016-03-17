using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyIoC;
using TinyService.Infrastructure;

namespace TinyService.TinyIoc
{
    public class TinyIocAdapter : IObjectContainer
    {
        private readonly TinyIoCContainer _container;

        public TinyIocAdapter(TinyIoCContainer container)
        {
            this._container = container;
        }

        public TinyIoCContainer Container
        {
            get
            {
                return this._container;
            }
        }

        public void Register<TService, TImplementer>(LifeStyle life = LifeStyle.Singleton)
            where TService : class
            where TImplementer : class, TService
        {
            this._container.Register<TService, TImplementer>();
        }

        public void RegisterInstance<TService, TImplementer>(Func<TService> factory = null)
            where TService : class
            where TImplementer : class, TService
        {
            this._container.Register<TService,TImplementer>((TImplementer)factory());
        }

        public void RegisterInstance<TService, TImplementer>(TImplementer instance, LifeStyle life = LifeStyle.Singleton)
            where TService : class
            where TImplementer : class, TService
        {
            throw new NotImplementedException();
        }

        public void RegisterType(Type serviceType, Type implementationType, LifeStyle life = LifeStyle.Singleton)
        {
            

            this._container.Register(serviceType, implementationType);
        }

        public void RegisterType(Type implementationType, LifeStyle life = LifeStyle.Singleton)
        {
            this._container.Register(implementationType);
        }

        public object Resolve(Type serviceType)
        {
            return this._container.Resolve(serviceType);
        }

        public TService Resolve<TService>() where TService : class
        {
            return this._container.Resolve<TService>();
        }

        public void Dispose()
        {
            
        }
    }
}
