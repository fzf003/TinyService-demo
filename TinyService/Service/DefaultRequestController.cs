using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Infrastructure;
using TinyService.Infrastructure.RequestHandler;
using TinyService.Infrastructure.Proxy;
namespace TinyService.Service
{
    /*   [Component(IsSingleton = true)]
      public class DefaultRequestServiceController : AbstractCommandService
      {
        
          protected override IAsyncRequestHandler<TRequest, TResponse> GetAsyncCommandHandler<TRequest, TResponse>()
          {
              var handler= ObjectFactory.GetService<IAsyncRequestHandler<TRequest, TResponse>>();
              if(handler==null)
              {
                  throw new Exception("handler 没有找到");
              }
              return handler;
          }


          protected override IRequestHandler<TRequest, TResponse> GetCommandHandler<TRequest, TResponse>()
          {
            var handler=  ObjectFactory.GetService<IRequestHandler<TRequest, TResponse>>();
              if (handler == null)
              {
                  throw new Exception("handler 没有找到");
              }
              return handler;
          }

          protected override IAsyncRequestHandler<TRequest> GetAsyncCommandHandler<TRequest>()
          {
              var handler = ObjectFactory.GetService<IAsyncRequestHandler<TRequest>>();
              if (handler == null)
              {
                  throw new Exception("handler 没有找到");
              }
              return handler;
          }

          protected override IRequestHandler<TRequest> GetCommandHandler<TRequest>()
          {
              var handler = ObjectFactory.GetService<IRequestHandler<TRequest>>();
              if (handler == null)
              {
                  throw new Exception("handler 没有找到");
              }
              return handler;
          }
       }*/
}
