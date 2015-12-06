using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Infrastructure.RequestHandler;

namespace TinyService.Application
{
    public interface IBlogService : IAsyncRequestHandler<QueryBlogRequest>,
                                    IRequestHandler<QueryBlogRequest>,
                                    IRequestHandler<QueryBlogRequest,Result>,
                                    IAsyncRequestHandler<QueryBlogRequest,Result>
    {

    }


    public class BlogService : IBlogService
    {
        public Task HandleAsync(QueryBlogRequest message)
        {
            Console.WriteLine("异步步不返回参数");
            return Task.FromResult("oopp");
        }

        public void Handle(QueryBlogRequest message)
        {
            Console.WriteLine("同步不返回参数");
        }

     

        Result IRequestHandler<QueryBlogRequest, Result>.Handle(QueryBlogRequest message)
        {
            Console.WriteLine("同步返回参数");
            return new Result()
            {
                IsSuccess = true,
                errors = new string[] { "a", "c" }
            };
        }

        Task<Result> IAsyncRequestHandler<QueryBlogRequest, Result>.HandleAsync(QueryBlogRequest message)
        {
            Console.WriteLine("异步步返回参数");
            return Task.FromResult(new  Result()
            {
                IsSuccess = true,
                errors = new string[] { "a", "c" }
            });
        }
    }

    public class Result
    {
        public bool IsSuccess { get; set; }

        public int Count { get; set; }

        public string[] errors { get; set; }
    }

    public class AddRess
    {
        public string Name { get; set; }
    }

    public class QueryBlogRequest : IRequest<AddRess>
    {

        public string Id
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public DateTime Timestamp
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
