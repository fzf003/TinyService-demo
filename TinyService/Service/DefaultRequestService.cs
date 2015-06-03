//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TinyService.Infrastructure.RequestHandler;

//namespace TinyService.Service
//{
//    public class DefaultRequestService : AbstractRequestController
//    {
//        protected override IAsyncRequestHandler<TRequest, TResponse> GetAsyncCommandHandler<TRequest, TResponse>()
//        {
//            return ObjectFactory.GetService<IAsyncRequestHandler<TRequest, TResponse>>();
//        }


//        protected override IRequestHandler<TRequest, TResponse> GetCommandHandler<TRequest, TResponse>()
//        {
//            return ObjectFactory.GetService<IRequestHandler<TRequest, TResponse>>();
//        }


//    }
//}
