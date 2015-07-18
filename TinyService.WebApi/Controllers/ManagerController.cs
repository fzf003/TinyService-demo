namespace TinyService.WebApi.Controllers
{
    using System.Web.Http;
    using TinyService.Domain.Repository;
    using TinyService.WebApi.Domain;
    using TinyService.Extension.Repository;
    using System.Collections.Generic;
    using System.Collections;
    using TinyService.Infrastructure;
    using System.Threading.Tasks;
    using System.Linq.Expressions;
    using System.Linq;
    using TinyService.Infrastructure.RequestHandler;
    using TinyService.Infrastructure.RegisterCenter;
    using TinyService.WebApi.Handler;
    using System;
    using TinyService.Infrastructure.Log;

    public class ManagerController : ApiController
    {

        private readonly IRequestServiceController _servicecontroller;
        private readonly ILogger Logger ;
        public ManagerController()
            : this(ObjectFactory.GetService<IRequestServiceController>())
        {
            Logger=ObjectFactory.GetService<ILoggerFactory>().Create(typeof(ManagerController));
        }

        public ManagerController(IRequestServiceController servicecontroller)
        {
            this._servicecontroller = servicecontroller;
        }

        [Route("api/Manager")]
        public async Task< IHttpActionResult> GetManagerList()
        {
           
            var result = await this._servicecontroller.SendAsync<QueryAllManager, IList<Manager>>(new QueryAllManager());
            Logger.Info(Newtonsoft.Json.JsonConvert.SerializeObject(result));
            return Ok(result);
        }
        [Route("api/Add")]
        [HttpGet]
        public async Task<IHttpActionResult> AddManager()
        {
            var result = await this._servicecontroller.SendAsync<AddManager, Result>(new AddManager()
              {
                  Body = new Manager() { Title = "我是管理者" }
              });
     

            return this.Ok(new { Success = true, body = result });
        }

        [Route("api/GetCount")]
        public async Task<IHttpActionResult> GetCount()
        {
            var count= await this._servicecontroller.SendAsync<CountManager,Result>(new CountManager());

            return this.Ok(  new { Total=count  });
        }
 
    }
}
