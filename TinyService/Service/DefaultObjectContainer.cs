using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Infrastructure;

namespace TinyService.Service
{
    public class DefaultObjectContainer : IObjectContainer
    {
        private readonly static ConcurrentDictionary<Type, Registration> TypeResository = new ConcurrentDictionary<Type, Registration>();
        private readonly static ConcurrentDictionary<Type, object> InstanceResository = new ConcurrentDictionary<Type, object>();
        private static volatile DefaultObjectContainer _Instance = null;
  
        public DefaultObjectContainer() { }

        public void RegisterType(Type implementationType, LifeStyle life = LifeStyle.Singleton)
        {
            var registration = new Registration
            {
                TImplementerType = implementationType,
                life = life
            };

            SaveImpltype(registration, life);
        }

        public void RegisterType(Type serviceType, Type implementationType, LifeStyle life = LifeStyle.Singleton)
        {
            var registration = new Registration
            {
                TServiceType = serviceType,
                TImplementerType = implementationType,
                life = life
            };

            Save(registration, life);
        }

        public void Register<TService, TImplementer>(LifeStyle life = LifeStyle.Singleton)
            where TService : class
            where TImplementer : class,TService
        {

            var registration = new Registration
            {
                TServiceType = typeof(TService),
                TImplementerType = typeof(TImplementer),
                life = life
            };

            Save<TService>(registration, life);
        }

        public void RegisterInstance<TService, TImplementer>(Func<TService> factory = null)
            where TService : class
            where TImplementer : class,TService
        {
            var registration = new Registration
            {
                TServiceType = typeof(TService),
                TImplementerType = typeof(TImplementer),
                Factory = factory
            };

            Save<TService>(registration, LifeStyle.Transient);
        }


        public void RegisterInstance<TService, TImplementer>(TImplementer instance, LifeStyle life = LifeStyle.Singleton)
            where TService : class
            where TImplementer : class, TService
        {
            var registration = new Registration
            {
                TServiceType = typeof(TService),
                TImplementerType = typeof(TImplementer),
                Implinstace = instance
            };

            Save<TService>(registration, life);
        }


        public object Resolve(Type serviceType)
        {
            object instance = new object();

            if (TypeResository.ContainsKey(serviceType))
            {
                var registration = TypeResository[serviceType];

                if (registration.life == LifeStyle.Transient)
                {
                    instance = registration.Instace();
                }
                else if (registration.life == LifeStyle.Singleton)
                {
                    instance = InstanceResository[serviceType];
                }
            }
            return instance;
        }

        public TService Resolve<TService>() where TService : class
        {
            TService instance = default(TService);

            if (TypeResository.ContainsKey(typeof(TService)))
            {
                var registration = TypeResository[typeof(TService)];

                if (registration.life == LifeStyle.Transient)
                {
                    instance = registration.Instace<TService>();
                }
                else if (registration.life == LifeStyle.Singleton)
                {
                    instance = (TService)InstanceResository[typeof(TService)];
                }
            }
            return instance;
        }

        /// <summary>
        /// 保存实例或类型
        /// </summary>
        /// <param name="registration"></param>
        /// <param name="life"></param>
        static void Save<TService>(Registration registration, LifeStyle life = LifeStyle.Singleton)
        {
            if (life == LifeStyle.Singleton)
            {

                InstanceResository.TryAdd(registration.TServiceType, registration.Instace<TService>());
            }
            else if (life == LifeStyle.Transient)
            {
                if (registration.Implinstace != null)
                {
                    InstanceResository.TryAdd(registration.TServiceType, registration.Implinstace);
                }

            }

            TypeResository.TryAdd(registration.TServiceType, registration);
        }

        static void Save(Registration registration, LifeStyle life = LifeStyle.Singleton)
        {
            if (life == LifeStyle.Singleton)
            {
                InstanceResository.TryAdd(registration.TServiceType, registration.Instace());

            }
            else if (life == LifeStyle.Transient)
            {
                if (registration.Implinstace != null)
                {
                    InstanceResository.TryAdd(registration.TServiceType, registration.Implinstace);
                }

            }

            TypeResository.TryAdd(registration.TServiceType, registration);
        }




        /// <summary>
        /// 登记具体实现类型
        /// </summary>
        /// <param name="registration"></param>
        /// <param name="life"></param>
        static void SaveImpltype(Registration registration, LifeStyle life = LifeStyle.Singleton)
        {
            if (life == LifeStyle.Singleton)
            {
                InstanceResository.TryAdd(registration.TImplementerType, registration.Instace());

            }
            else if (life == LifeStyle.Transient)
            {
                InstanceResository.TryAdd(registration.TImplementerType, registration.Implinstace);
            }

            TypeResository.TryAdd(registration.TImplementerType, registration);
        }





        public void Dispose()
        {
             
        }
    }

    /// <summary>
    /// 注册类型元数据
    /// </summary>
    struct Registration
    {
        public Type TServiceType;
        public Type TImplementerType;
        public Func<object> Factory;
        public LifeStyle life;
        public object Implinstace;

        public TService Instace<TService>(params object[] args)
        {
            if (Factory != null)
                return (TService)Factory();
            if (Implinstace == null)
            {
                return (TService)Activator.CreateInstance(TImplementerType, args);
            }
            else
            {
                return (TService)Implinstace;
            }

        }

        public object Instace(params object[] args)
        {
            if (Factory != null)
                return Factory();

            if (Implinstace == null)
            {
                return Activator.CreateInstance(TImplementerType, args);
            }
            else
            {
                return Implinstace;
            }

        }
    }
}
