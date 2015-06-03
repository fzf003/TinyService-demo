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
  
    public class ManagerController : ApiController
    {
        private readonly IRepository<string, Manager> _store;
        public ManagerController()
        {
            this._store = ObjectFactory.GetService<IRepository<string, Manager>>();
            
        }

          [Route("api/Manager")]
        public async Task<IHttpActionResult> GetValues()
        {
            
            return Ok(this._store.GetAll().ToList());
        }
         [Route("api/Add")]
       [HttpGet]
        public async Task<IHttpActionResult> AddManager()
          {
            await this._store.InsertAsync(new Manager() { Title = "我是管理者" });
            return this.Ok(new { Success = true });
          }
    }
}
