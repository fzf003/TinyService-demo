using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using TinyService.autofac;
using System.Reflection;
using TinyService.Infrastructure.Process.Actor;
using Autofac;
using Microsoft.AspNet.SignalR;
using TinyService.MessageBus;
[assembly: OwinStartup(typeof(TinyService.Application.Startup))]

namespace TinyService.Application
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
       
            //GlobalHost.DependencyResolver.UseRedis("127.0.0.1", 6379, string.Empty, "SignalRBus");

            var configuration = new HubConfiguration() { EnableDetailedErrors = true, EnableJSONP = true };
            app.MapSignalR(configuration);
            Init();
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
                         );
 
                    containerbuilder.RegisterInstance(ActorApplication.Instanse);
                    containerbuilder.Register<TestActorService>(p => new TestActorService(p.Resolve<ActorApplication>()));
                     containerbuilder.RegisterType<BlogService>().AsImplementedInterfaces();

                 });

            });
        }
    }
}
