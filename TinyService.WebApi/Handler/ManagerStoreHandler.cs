using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TinyService.Domain.Repository;
using TinyService.Infrastructure.RequestHandler;
using TinyService.WebApi.Domain;
using TinyService.Extension.Repository;
using System.Threading.Tasks;
using TinyService.Infrastructure;
using TinyService.Validator;
using TinyService.Infrastructure.Proxy;

namespace TinyService.WebApi.Handler
{
 
    public interface IManagerCommandService:IAsyncRequestHandler<AddManager,Result>,
                                          IAsyncRequestHandler<QueryAllManager, IList<Manager>>,
                                          IAsyncRequestHandler<CountManager, Result>
                                          
    {

    }

    [Component]
    public class ManagerCommandService : IManagerCommandService
    {
        private readonly IRepository<string, Manager> _store;
        private readonly TinyServiceValidatorFactory _factory;
        public ManagerCommandService(IRepository<string, Manager> store, TinyServiceValidatorFactory factory)
        {
            this._store = store;
            this._factory = factory;
        }


        public async System.Threading.Tasks.Task<Result> HandleAsync(AddManager message)
        {
            var result = _factory.GetValidator<AddManager>();
                
               var validresult= await result.ValidateAsync(message);

               if (!validresult.IsValid)
               {
                 var Responseresult=  new Result()
                   {
                       errors = validresult.Errors.Where(p => p != null)
                                    .Select(p => string.Format("{0}:{1}", p.PropertyName, p.ErrorMessage))
                                    .ToArray()
                    };
                   Responseresult.IsSuccess=!(Responseresult.errors.Count()>0);
                   Responseresult.Count=Responseresult.errors.Count();
                   return Responseresult;
               }
               else
               {
                   
                   await this._store.InsertAsync(message.Body);
                   return new Result() { IsSuccess = true };
               }
        }

        public IList<Manager> Handle(QueryAllManager message)
        {
            return this._store.GetAll().ToList();
        }
       
        public async Task<Result> HandleAsync(CountManager message)
        {
            return new Result() {
             IsSuccess=true,
              Count= await this._store.CountAsync()
            };
        }

        public async Task<IList<Manager>> HandleAsync(QueryAllManager message)
        {
              return await Task.FromResult<IList<Manager>>(this._store.GetAll().ToList());
        }
    }
}