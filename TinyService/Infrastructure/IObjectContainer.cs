using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyService.Infrastructure
{
    //public interface IObjectContainer
    //{
    //    // TService Resolve<TService>() where TService : class;


    //}

    public interface IObjectContainer:IDisposable
    {
        void RegisterType(Type implementationType, LifeStyle life = LifeStyle.Singleton);

        void RegisterType(Type serviceType, Type implementationType, LifeStyle life = LifeStyle.Singleton);

        void Register<TService, TImplementer>(LifeStyle life = LifeStyle.Singleton)
            where TService : class
            where TImplementer : class, TService;


        void RegisterInstance<TService, TImplementer>(TImplementer instance, LifeStyle life = LifeStyle.Singleton)
            where TService : class
            where TImplementer : class, TService;

       void RegisterInstance<TService, TImplementer>(Func<TService> factory = null)
            where TService : class
            where TImplementer : class, TService;

        TService Resolve<TService>() where TService : class;

        object Resolve(Type serviceType);
    }


    public enum LifeStyle
    {
        /// <summary>
        /// 瞬时对像
        /// </summary>
        Transient,
        /// <summary>
        /// 单例
        /// </summary>
        Singleton
    }
}
