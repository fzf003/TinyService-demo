using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TinyService.Domain.Repository;
using TinyService.Infrastructure;
using TinyService.Infrastructure.RequestHandler;
using TinyService.Validator;
using TinyService.WebApi.Domain;
using TinyService.WebApi.Models;
using TinyService.Extension.Repository;
using TinyService.WebApi.Models.Request;
using System.Threading.Tasks;

namespace TinyService.WebApi.Handler
{
    public interface IBlogCommandService : IAsyncRequestHandler<BlogPostRequest, Result>,
                                           IAsyncRequestHandler<QueryBlogRequest, Result>
                                   
    {

    }

    [Component]
    public class BlogCommandService : IBlogCommandService
    {
         private readonly IRepository<int, BlogPost> _store;
         private readonly TinyServiceValidatorFactory _factory;
        public BlogCommandService(IRepository<int, BlogPost> store, TinyServiceValidatorFactory factory)
        {
            this._store = store;
            this._factory = factory;
        }
      


         public async System.Threading.Tasks.Task<Result> HandleAsync(BlogPostRequest message)
         {
             var result = new Result();
             var isvalidate =await this._factory.GetValidator<BlogPostRequest>().ValidateAsync(message);
             if (!isvalidate.IsValid)
             {
                 var errors = isvalidate.Errors.Select(p => string.Format("{0}:{1}", p.PropertyName, p.ErrorMessage)).ToArray();
                 result.IsSuccess = errors.Count() > 0;
                 result.errors = errors;
                 return result;
             }
             
             var blog =  this._store.InsertOrUpdate(message.Post);
             result.IsSuccess = true;
             result.Count = 200;
             return result;
         }

         public async System.Threading.Tasks.Task<Result> HandleAsync(QueryBlogRequest message)
         {
             var result = new Result();
             result.Blogs = this._store.GetAll().ToList();
            
             return await Task.FromResult<Result>(result);
         }
    }

}