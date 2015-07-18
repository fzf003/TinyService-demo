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

            TinyServiceSetup.Start((configbulider) =>
            {
                configbulider.InitConfig((containerbuilder) =>
                {

                    containerbuilder.RegisterComponents(
                          Assembly.GetExecutingAssembly(),
                          Assembly.Load("TinyService"),
                          Assembly.Load("TinyService.Log4Net"),
                          Assembly.Load("TinyService.autofac")
                          );

                    //containerbuilder.RegisterType<DefaultLocalServiceBus>().SingleInstance().AsImplementedInterfaces();
                    //containerbuilder.RegisterType<InMomeryRepository>().SingleInstance().As<IRepository<string, Manager>>();
                    //containerbuilder.RegisterType<DefaultRequestServiceController>().SingleInstance().AsImplementedInterfaces();
                    //containerbuilder.RegisterType<ManagerStoreService>().AsImplementedInterfaces();
                });
            });

            ActorProcessRegistry.Instance.AddActor(typeof(RequestActor), new RequestActor());

           
        }



    }
}
