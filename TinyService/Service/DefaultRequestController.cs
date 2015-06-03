using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Infrastructure;
using TinyService.Infrastructure.RequestHandler;

namespace TinyService.Service
{
    public class DefaultRequestServiceController : AbstractRequestServiceController
    {
        protected override IAsyncRequestHandler<TRequest, TResponse> GetAsyncCommandHandler<TRequest, TResponse>()
        {
            return ObjectFactory.GetService<IAsyncRequestHandler<TRequest, TResponse>>();
        }


        protected override IRequestHandler<TRequest, TResponse> GetCommandHandler<TRequest, TResponse>()
        {
            return ObjectFactory.GetService<IRequestHandler<TRequest, TResponse>>();
        }


    }
}
