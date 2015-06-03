using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyService.Infrastructure
{
    public class ObjectFactory
    {
        static IObjectContainer Current { get;  set; }

        public static void SetContainer(IObjectContainer container)
        {
            Current = container;
        }

        public static T GetService<T>()
        {
            return ServiceLocator.Current.GetInstance<T>();
        }

        public static T GetService<T>(string key)
        {
            return ServiceLocator.Current.GetInstance<T>(key);
        }


        public static IServiceLocator Servicelocator
        {
            get
            {
                return ServiceLocator.Current;
            }
        }

    }
}
