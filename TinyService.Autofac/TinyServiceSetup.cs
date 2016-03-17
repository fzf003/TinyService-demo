using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TinyService.Infrastructure;
 
namespace TinyService.autofac
{
    public  class TinyServiceSetup
    {
        static Lazy<TinyServiceSetup> _instance = new Lazy<TinyServiceSetup>(() => new TinyServiceSetup());

      

        private TinyServiceSetup()
        {

        }
        public static TinyServiceSetup Instance
        {
            get
            {
                return _instance.Value;
            }
        }
        public IContainer Container
        {
            get;
            set;
        }

        public  TinyServiceSetup Start(Action<IConfigurationBuilder> configurator)
        {
            var builder = new ContainerBuilder();
            var config = new AutoFacConfigurationBuilder(builder);
            configurator(config);
            var adapter= new AutofacAdapter(builder);
            this.Container = adapter.Container;
            ObjectFactory.SetContainer(new AutofacServiceLocator(adapter.Container));
            return this;
        }

        public  TinyServiceSetup Start(Action action)
        {
            action();

            return this;
                
        }

   
     
    }
}
