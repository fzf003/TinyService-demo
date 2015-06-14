using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TinyService.Domain.Repository;
using TinyService.Infrastructure;
using TinyService.Infrastructure.Process;
using TinyService.WebApi.Domain;

namespace TinyService.WebApi.Handler
{
    public class RequestActor : Actor
    {
        private readonly IRepository<string, Manager> _store;
        public RequestActor(IRepository<string, Manager> store)
        {
            this._store = store;
        }
        public RequestActor()
            : this(ObjectFactory.GetService<IRepository<string, Manager>>())
        {

        }

        public void Handle(AddManager message)
        {
            this._store.Insert(message.Body);
        }
    }

}