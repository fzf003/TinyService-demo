﻿using Microsoft.Owin;
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

    using TinyService.Infrastructure;
    using Microsoft.Practices.ServiceLocation;
    using Autofac;
    using TinyService.autofac;
    using System;
    using TinyService.Service;
    using TinyService.WebApi.Repository;
    using Autofac.Integration.WebApi;
    using TinyService.Domain.Repository;
    using TinyService.WebApi.Domain;
    using System.Reflection;
    using TinyService.WebApi.Handler;

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

            // Make ./public the default root of the static files in our Web Application.
            app.UseFileServer(new FileServerOptions
            {
                RequestPath = new PathString(string.Empty),
                FileSystem = new PhysicalFileSystem("./public"),
                EnableDirectoryBrowsing = true,
            });

            app.UseStageMarker(PipelineStage.MapHandler);
            httpConfiguration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

            InitTinyService((containerbuilder) =>
            {
                containerbuilder.RegisterType<DefaultLocalServiceBus>().SingleInstance().AsImplementedInterfaces();
                containerbuilder.RegisterType<InMomeryRepository>().SingleInstance().As<IRepository<string, Manager>>();
                containerbuilder.RegisterType<DefaultRequestServiceController>().SingleInstance().AsImplementedInterfaces();
                containerbuilder.RegisterType<ManagerStoreService>().AsImplementedInterfaces();


            });
        }

        static void InitTinyService(Action<ContainerBuilder> action)
        {
            var containerbuilder = new ContainerBuilder();
            action(containerbuilder);
            var container = containerbuilder.Build();
            ObjectFactory.SetContainer(new AutofacAdapter(container));
            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(container));
        }

        
    }
}
