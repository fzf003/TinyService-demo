using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyService.Infrastructure.RequestHandler
{
    public abstract class AbstractCommandService : ICommandService
    {
        public Task<TResponse> SendAsync<TRequest, TResponse>(TRequest request)
            where TRequest : class,IRequest
            where TResponse : class
        {
            return GetAsyncCommandHandler<TRequest, TResponse>().HandleAsync(request);
        }

        public TResponse Send<TRequest, TResponse>(TRequest request)
            where TRequest : class, IRequest
            where TResponse : class
        {
            return GetCommandHandler<TRequest, TResponse>().Handle(request);
        }

        protected abstract IAsyncRequestHandler<TRequest, TResponse> GetAsyncCommandHandler<TRequest, TResponse>()
            where TRequest : class,IRequest
            where TResponse : class;



        protected abstract IRequestHandler<TRequest, TResponse> GetCommandHandler<TRequest, TResponse>()
            where TRequest : class,IRequest
            where TResponse : class;



        public void Send<TRequest>(TRequest request) where TRequest : class, IRequest
        {
            GetCommandHandler<TRequest>().Handle(request);
        }

        protected abstract IRequestHandler<TRequest> GetCommandHandler<TRequest>()
            where TRequest : class,IRequest;

        Task ICommandService.SendAsync<TRequest>(TRequest request)
        {
            return GetAsyncCommandHandler<TRequest>().HandleAsync(request);
        }

        protected abstract IAsyncRequestHandler<TRequest> GetAsyncCommandHandler<TRequest>()
            where TRequest : class,IRequest;
            
    }
}
