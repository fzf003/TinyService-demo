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
using TinyService.Infrastructure.Proxy;
using TinyService.Application.Services;
using TinyService.Infrastructure.Log;
using TinyService.TinyIoc;
using TinyIoC;
using TinyService.Log4Net;
using TinyService.Caching;
using TinyService.Validator;
using TinyService.Application.Models;
using FluentValidation;
using Castle.DynamicProxy;
using TinyService.Infrastructure.Process.Actor;
using TinyService.Infrastructure.RequestHandler;
using System.Reactive.Threading.Tasks;
using Microsoft.Owin.Hosting;
using TinyService.MessageBus.Contract;
using TinyService.MessageBus.Impl;
using TinyService.MessageBus;
namespace TinyService.Application
{
    class Program
    {
        static ProxyGenerator proxyGenerator = new ProxyGenerator();

        static void Main(string[] args)
        {

            using (WebApp.Start<Startup>("http://localhost:9090/"))
            {
                
                Console.WriteLine("Server running at http://localhost:9090/");
                Console.ReadKey();
            }


            Console.WriteLine("......");
            Console.ReadKey();
        }

        static void Validator()
        {
            var factory = ObjectFactory.GetService<TinyServiceValidatorFactory>();
            List<IValidator<Person>> _validators = new List<IValidator<Person>>();
            _validators.Add(factory.GetValidator<Person>());

            var failures =
               _validators.Select(v => v.Validate(new ValidationContext(new Person())))
                          .SelectMany(r => r.Errors)
                          .Where(f => f != null).ToList();

            failures.ForEach(p =>
            {
                Console.WriteLine(p.PropertyName + "==" + p.ErrorMessage);
            });
        }

        static void ServiceBus()
        {
           
            var bus = ObjectFactory.GetService<IServiceBus>();

            bus.ToSubscribe<AppMessage>((item) =>
            {
                Console.WriteLine(item.Id + "|" + item.Timestamp);
            });

            bus.RegisterMessageHandler<AppMessage>(message =>
            {
                Console.WriteLine(message.Id + "|" + message.Timestamp+"|"+Thread.CurrentThread.ManagedThreadId);
            });

          
                List<AppMessage> store=new List<AppMessage>();
                store.Add(new AppMessage() { 
                
                });

                bus.Send(new AppMessage()
                {

                });
         
            

        }

        static void TinyIocInit()
        {
            var container = TinyIoCContainer.Current;

            container.AutoRegister(new List<Assembly>() {
                          Assembly.Load("TinyService"),
                          Assembly.Load("TinyService.Log4Net"),
             });

            //container.Register<TinyServiceValidatorFactory>().AsSingleton();
            //container.Register<ProxyGenerator>().AsSingleton();
            container.Register(ActorApplication.Instanse);
            container.Register<TestActorService>((c, p) => new TestActorService(c.Resolve<ActorApplication>()));
            ServiceLocator.SetLocatorProvider(() => new TinyIoCServiceLocator(container));
        }

        static void Init()
        {
            TinyServiceSetup.Instance.Start((configbulider) =>
            {
                configbulider.InitConfig((containerbuilder) =>
                {
                    containerbuilder.RegisterComponents(
                        Assembly.GetExecutingAssembly(),
                        Assembly.Load("TinyService"),
                        Assembly.Load("TinyService.Log4Net")
                        //typeof(ServiceBase).Assembly,
                        //typeof(ILoggerFactory).Assembly
                        );
                    containerbuilder.RegisterType<ProxyGenerator>().SingleInstance();

                    containerbuilder.RegisterInstance(ActorApplication.Instanse);
                    containerbuilder.Register<TestActorService>(p => new TestActorService(p.Resolve<ActorApplication>()));
                    //containerbuilder.RegisterType<DefaultRequestServiceController>().SingleInstance().AsImplementedInterfaces();
                    containerbuilder.RegisterType<BlogService>().AsImplementedInterfaces();

                    //containerbuilder.Register<TinyServiceValidatorFactory>().AsSingleton();
                });

            });
        }



    }

    public static class MapperExt
    {
        public static IConfigurationBuilder UseMapper(this IConfigurationBuilder configbulider, Action action)
        {
            if (configbulider != null)
            {
                action();
            }
            return configbulider;
        }
    }

    public static class Ext
    {
        public static T CreateTempProxy<T>(this object target, params IInterceptor[] interceptorProxy)
        {
            var _proxyGenerator = ServiceLocator.Current.GetInstance<ProxyGenerator>();

            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            var targetType = target.GetType();

            var targetInterfaces = targetType.GetInterfaces();

            if (targetInterfaces.Any())
            {
                var proxy = _proxyGenerator.CreateInterfaceProxyWithTargetInterface(targetInterfaces.Last(), target, interceptorProxy);
                return (T)proxy;
            }
            else
            {
                var greediestCtor = targetType.GetConstructors().OrderBy(x => x.GetParameters().Count()).LastOrDefault();
                var ctorDummyArgs = greediestCtor == null ? new object[0] : new object[greediestCtor.GetParameters().Count()];
                var proxy = _proxyGenerator.CreateClassProxyWithTarget(targetType, target, ctorDummyArgs, interceptorProxy);
                return (T)proxy;
            }
        }
    }
}
