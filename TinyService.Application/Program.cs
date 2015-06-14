using Autofac;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Infrastructure;
using TinyService.Service;
using System.Reactive.Linq;
using System.Reactive;
using System.Reactive.Subjects;
using TinyService.Extension.ServiceBus;
using System.Threading;
using System.Reflection;
using TinyService.autofac;
using CommonComposition;
namespace TinyService.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            Init();

            var bus = ObjectFactory.GetService<IServiceBus>();

            bus.Subscribe<AppMessage>((item) =>
            {
                Console.WriteLine(item.Id + "|" + item.Timestamp);
            });

            bus.RegisterMessageHandler<AppMessage>(message =>
            {
                Console.WriteLine(message.Id + "|" + message.Timestamp);
            });


            bus.PublishAsync<AppMessage>(new AppMessage() { });

            Console.WriteLine("......");
            Console.ReadKey();
        }

        static void Init()
        {
            TinyServiceSetup.Start((configbulider) =>
            {
                configbulider.InitConfig((containerbuilder) =>
                  {
                      containerbuilder.RegisterType<DefaultLocalServiceBus>().SingleInstance().AsImplementedInterfaces();
                      containerbuilder.RegisterType<DefaultRequestServiceController>().SingleInstance().AsImplementedInterfaces();
                      containerbuilder.RegisterComponents(typeof(Foo).Assembly).InstancePerRequest().AsImplementedInterfaces();
                  })
                  .UseMapper(() => { 
                  
                  });
            });
        }


       
    }

    public static class MapperExt
    {
        public static IConfigurationBuilder UseMapper(this IConfigurationBuilder configbulider,Action action)
        {
            if (configbulider != null)
            {
                action();
            }
            return configbulider;
        }
    }
}
