using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TinyService.Domain.Repository;
using TinyService.Infrastructure.RequestHandler;
using TinyService.WebApi.Domain;
using TinyService.Extension.Repository;
using System.Threading.Tasks;

namespace TinyService.WebApi.Handler
{
    public interface IManagerStoreService:IAsyncRequestHandler<AddManager,Result>,
                                          IRequestHandler<QueryAllManager,IList<Manager>>,
                                          IAsyncRequestHandler<CountManager, Result>
                                          
    {

    }

    public class ManagerStoreService:IManagerStoreService
    {
        private readonly IRepository<string, Manager> _store;
        public ManagerStoreService(IRepository<string, Manager> store)
        {
            this._store = store;
        }


        public async System.Threading.Tasks.Task<Result> HandleAsync(AddManager message)
        {
            await this._store.InsertAsync(message.Body);
            return new Result() { IsSuccess=true };
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



       
    }
}