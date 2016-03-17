using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyService.Infrastructure.RequestHandler
{
    public interface IRequest : IMessage { }


    public interface IRequest<out T> : IRequest
    {

    }


    public interface IRequestHandler<TRequest, TResponse>
        where TRequest : class,IRequest
        where TResponse : class
    {
        TResponse Handle(TRequest message);
    }

    public interface IRequestHandler<TRequest>
         where TRequest : class,IRequest
    {
        void Handle(TRequest message);
    }


    public interface IAsyncRequestHandler<TRequest, TResponse>
        where TRequest : class,IRequest
        where TResponse : class
    {
        Task<TResponse> HandleAsync(TRequest message);
    }

    public interface IAsyncRequestHandler<TRequest>
        where TRequest : class,IRequest
    {
        Task HandleAsync(TRequest message);
    }


}
