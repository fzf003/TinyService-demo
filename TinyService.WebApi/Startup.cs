using Microsoft.Owin;
using TinyService.WebApi;

[assembly: OwinStartup(typeof(Startup))]
namespace TinyService.WebApi
{
    using System.Web.Http;
    using Microsoft.Owin;
    using Microsoft.Owin.Extensions;
    using Microsoft.Owin.FileSystems;
    using Microsoft.Owin.StaticFiles;
    using Owin;

    using Microsoft.Practices.ServiceLocation;
    using Autofac;
    using System;
    using Autofac.Integration.WebApi;
    using System.Reflection;
    using TinyService.Service;
    using TinyService.Domain.Repository;
    using TinyService.WebApi.Repository;
    using TinyService.WebApi.Handler;
    using TinyService.WebApi.Domain;
    using TinyService.Infrastructure.RegisterCenter;
    using TinyService.autofac;
    using TinyService.Infrastructure;
    using TinyService.Validator;
    using TinyService.WebApi.Models;
    using System.Web.Mvc;
    using TinyService.MessageBus.Impl;
    using TinyService.MessageBus;
    using TinyService.MessageBus.Contract;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var httpConfiguration = new HttpConfiguration();

            // Configure Web API Routes:
            // - Enable Attribute Mapping
            // - Enable Default routes at /api.
            httpConfiguration.MapHttpAttributeRoutes();
            httpConfiguration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            app.UseWebApi(httpConfiguration);

            app.UseFileServer(new FileServerOptions
            {
                RequestPath = new PathString(string.Empty),
                FileSystem = new PhysicalFileSystem("./public"),
                EnableDirectoryBrowsing = true,
            });
            
            app.UseStageMarker(PipelineStage.MapHandler);
            httpConfiguration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();


           

            TinyServiceSetup.Instance.Start((configbulider) =>
            {
                  var config= new MessageBusConfiguration()
                                   {
                                       HubName = "ClientHub",
                                       QueryString = string.Empty,
                                       RemoteUrl = "http://localhost:9090/"
                                   };

                configbulider.InitConfig((containerbuilder) =>
                {

                    containerbuilder.RegisterComponents(
                          Assembly.GetExecutingAssembly(),
                          Assembly.Load("TinyService"),
                          Assembly.Load("TinyService.Log4Net"),
                          Assembly.Load("TinyService.autofac")
                          );
                     

                         containerbuilder.Register(p=>new DefaultMessageBus(config))
                                         .AsImplementedInterfaces()
                                         .SingleInstance();

             
                   
                   
                       
          
                });
            });

          /*  var bus= ObjectFactory.GetService<IMessageBus>();

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
                Console.WriteLine("Notify收到:" + message.Name + "|" + message.messageId);
            });

            bus.Subscribe<AppMessage>("Receive", (message) =>
            {
                Console.WriteLine("Receive收到:" + message.Name + "|" + message.messageId);
            });

            bus.Start().Wait();*/

        
        }



    }
}
