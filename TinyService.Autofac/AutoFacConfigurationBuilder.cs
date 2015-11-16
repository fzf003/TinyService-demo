using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Infrastructure;

namespace TinyService.autofac
{
    public interface IConfigurationBuilder
    {
        IConfigurationBuilder InitConfig(Action<Autofac.ContainerBuilder> initializer);
        
    }



    public class AutoFacConfigurationBuilder : IConfigurationBuilder
    {
        private readonly ContainerBuilder _containerBuilder;
        public AutoFacConfigurationBuilder(ContainerBuilder containerBuilder)
        {
            this._containerBuilder = containerBuilder;
        }

        public IConfigurationBuilder InitConfig(Action<ContainerBuilder> initializer)
        {
            initializer(this._containerBuilder);
            return this;
        }


      
    }
}
