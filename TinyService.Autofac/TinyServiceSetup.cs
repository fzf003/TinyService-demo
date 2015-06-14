using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Infrastructure;

namespace TinyService.autofac
{
    public static class TinyServiceSetup
    {
        private static IObjectContainer _container;


        public static IObjectContainer Container
        {
            get { return _container; }
        }

        public static void Start(Action<IConfigurationBuilder> configurator)
        {
            var builder = new ContainerBuilder();
            var config = new AutoFacConfigurationBuilder(builder);
            configurator(config);
            SetContainer(new AutofacAdapter(builder));
        }


        internal static void SetContainer(IObjectContainer container)
        {
            _container = container;
            ObjectFactory.SetContainer(new AutofacServiceLocator(((AutofacAdapter)container).Container));
        }
    }
}
