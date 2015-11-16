using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyService.Infrastructure.RequestHandler
{
    public interface ICommandService 
    {
        TResponse Send<TRequest, TResponse>(TRequest request)
            where TRequest : class, IRequest
            where TResponse : class;

        void Send<TRequest>(TRequest request) 
            where TRequest : class, IRequest;
          

        System.Threading.Tasks.Task<TResponse> SendAsync<TRequest, TResponse>(TRequest request)
            where TRequest : class, IRequest
            where TResponse : class;

        System.Threading.Tasks.Task SendAsync<TRequest>(TRequest request)
            where TRequest : class, IRequest;
          

         
    }
}
