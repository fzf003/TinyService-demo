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

    public class ManagerController : ApiController
    {

        private readonly IRequestServiceController _servicecontroller;
        public ManagerController()
            : this(ObjectFactory.GetService<IRequestServiceController>())
        {

        }

        public ManagerController(IRequestServiceController servicecontroller)
        {
            this._servicecontroller = servicecontroller;
        }

        [Route("api/Manager")]
        public async Task<IHttpActionResult> GetManagerList()
        {
            var result = this._servicecontroller.Send<QueryAllManager, IList<Manager>>(new QueryAllManager());
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
    }
}
