using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TinyService.autofac;
using TinyService.Command.Impl;
using TinyService.Domain.Entities;
using TinyService.DomainEvent.Impl;
using TinyService.Service;
using Autofac;
using TinyService.Infrastructure;
using Products_Core.Ports.Commands;
using Products_Core.Ports.Events;
using Products_Core.Ports.Handlers;
using TinyService.DomainEvent;
using Products_Core.Model;
using TinyService.Domain.Repository;
using System.Diagnostics;
using System.Threading.Tasks.Dataflow;
using TinyService.Command;
using System.Reactive.Linq;
using System.Collections.Concurrent;
using System.Reactive.Concurrency;
using TinyService;
using System.Reactive.Threading.Tasks;
using TinyService.Command;
namespace Product_API
{
    class Program
    {

        static void Main(string[] args)
        {
            Init();
           
            var bus = ObjectFactory.GetService<IServiceBus>();

            RegisterEventHandler(bus);

            bus.RegisterCommandType<AddProductCommand>();
            bus.RegisterCommandType<ChangeProductCommand>();
            bus.RegisterCommandType<RemoveProductCommand>();


            bus.SendAsync(new AddProductCommand(
                                    productName: "笔记本",
                                    productDescription: "这是一个电脑笔记本",
                                    productPrice: 899
                              )).ContinueWith(task =>
                              {
                                  Console.WriteLine(task.Status);
                                  if (task.Status == TaskStatus.Faulted)
                                  {
                                      Console.WriteLine("Error:{0}", task.Exception.InnerException.Message);
                                  }
                              });

             
             


            Console.Read();

            bus.SendAsync(new ChangeProductCommand("1", "ppo", "kMM", 892)).Wait();
            Task.Delay(50).Wait();
            bus.SendAsync(new RemoveProductCommand("1"));

            
            Console.WriteLine(".....");
            Console.ReadKey();
        }


        static void RegisterEventHandler(IServiceBus bus)
        {
            bus.ToSubscribe<ProductAddedEvent>(ObjectFactory.GetService<IDomainEventHandler<ProductAddedEvent>>().Handle);
            bus.ToSubscribe<ProductChangedEvent>(ObjectFactory.GetService<IDomainEventHandler<ProductChangedEvent>>().Handle);
            bus.ToSubscribe<ProductRemovedEvent>(ObjectFactory.GetService<IDomainEventHandler<ProductRemovedEvent>>().Handle);
        }

        static void Init()
        {
            TinyServiceSetup.Instance.Start((configbulider) =>
            {
                configbulider.InitConfig((containerbuilder) =>
                {
                    containerbuilder.RegisterComponents(
                        Assembly.GetExecutingAssembly(),
                        Assembly.Load("Products-Core"),
                        Assembly.Load("TinyService"),
                        Assembly.Load("TinyService.Log4Net")
                         );

                    containerbuilder.RegisterType<CommandResultProcessor>().AsSelf().SingleInstance();
                 });
            });
        }
    }
}
