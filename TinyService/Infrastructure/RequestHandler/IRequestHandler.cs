using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyService.Infrastructure.RequestHandler
{
    public interface IRequest { }


    public interface IRequestHandler<TRequest, TResponse>
        where TRequest : class,IRequest
        where TResponse : class
    {
        TResponse Handle(TRequest message);


    }


    public interface IAsyncRequestHandler<TRequest, TResponse>
        where TRequest : class,IRequest
        where TResponse : class
    {
        Task<TResponse> HandleAsync(TRequest message);
    }
   

}
