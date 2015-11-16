using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.MessageBus.Impl;
using System.Reactive;
using TinyService.MessageBus.Contract;
using TinyService.MessageBus;
namespace TinyService.MessageBusClient
{
    class Program
    {
        static IMessageBus bus;
        static IMessageBus bus1;
        static void Main(string[] args)
        {
            Client1();
            Client2();
            bus1.AddTopic("fzf003").Wait();
            bus.AddTopic("fzf003").Wait();


            var message = new AppMessage()
            {
                Name = Guid.NewGuid().ToString()
            };

            bus.Publish<AppMessage>(message);
            bus1.Publish<AppMessage>(message);



            Console.ReadKey();
            bus.Stop();
            bus1.Stop();
        }
        static void Client2()
        {
            bus1 = new DefaultMessageBus(GetConfig());
            {
                bus1.ErrorAsObservable().Subscribe((ex) =>
                {
                    Console.WriteLine("Err:" + ex.Message);
                });

                bus1.Subscribe<AppMessage>("Close", (message) =>
                {
                    Console.WriteLine("关闭收到:" + message.Name + "|" + message.messageId);
                });

                bus1.Subscribe<AppMessage>("Notify", (message) =>
                {
                    Console.WriteLine("bus1Notify收到:" + message.Name);
                });

                bus1.Subscribe<AppMessage>("Receive", (message) =>
                {
                    Console.WriteLine("Receive收到:" + message.Name + "|" + message.messageId);
                });

                bus1.Start().Wait();




            }
        }

        static void Client1()
        {
            bus = new DefaultMessageBus(GetConfig());
            {
                bus.ErrorAsObservable().Subscribe((ex) =>
                {
                    Console.WriteLine("Err:" + ex.Message);
                });

                bus.Subscribe<AppMessage>("Close", (message) =>
                {
                    Console.WriteLine("关闭收到:" + message.Name + "|" + message.messageId);
                });

                bus.Subscribe<AppMessage>("Notify", (message) =>
                {
                    Console.WriteLine("Notify收到:" + message.Name);
                });

                bus.Subscribe<AppMessage>("Receive", (message) =>
                {
                    Console.WriteLine("Receive收到:" + message.Name + "|" + message.messageId);
                });

                bus.Start().Wait();




            }
        }


        static MessageBusConfiguration GetConfig()
        {
            return new MessageBusConfiguration()
            {
                HubName = "ClientHub",
                QueryString = string.Empty,
                RemoteUrl = "http://localhost:9090/"
            };
        }
    }
}
