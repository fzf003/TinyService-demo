using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TinyService.Domain.Repository;
using TinyService.Infrastructure.RequestHandler;
using TinyService.WebApi.Domain;
using TinyService.Extension.Repository;

namespace TinyService.WebApi.Handler
{
    public interface IManagerStoreService:IAsyncRequestHandler<AddManager,Result>,
                                          IRequestHandler<QueryAllManager,IList<Manager>>
    {

    }
    public class ManagerStoreService:IManagerStoreService
    {
        private readonly IRepository<string, Manager> _store;
        public ManagerStoreService(IRepository<string, Manager> store)
        {
            this._store = store;
        }


        public async System.Threading.Tasks.Task<Result> AsyncHandle(AddManager message)
        {
            await this._store.InsertAsync(message.Body);
            return new Result() { IsSuccess=true };
        }

        public IList<Manager> Handle(QueryAllManager message)
        {
            return this._store.GetAll().ToList();
        }
    }
}