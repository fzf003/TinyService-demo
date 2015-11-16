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
    using TinyService.Validator;
    using TinyService.WebApi.Models.Request;
    using TinyService.WebApi.Models;
    using TinyService.Infrastructure.Proxy;
    using TinyService.MessageBus;
    using TinyService.MessageBus.Contract;
    using TinyService.MessageBus.Impl;

    public class ManagerController : ApiController
    {

        private readonly ICommandService _servicecontroller;
        private readonly ILogger Logger;
        private readonly TinyServiceValidatorFactory _factory;
        private static TinyService.MessageBus.IMessageBus bus;
        public ManagerController()
            : this(ObjectFactory.GetService<ICommandService>()
                 , ObjectFactory.GetService<TinyServiceValidatorFactory>())
        {
            Logger = ObjectFactory.GetService<ILoggerFactory>().Create(typeof(ManagerController));

        }

        public ManagerController(ICommandService servicecontroller, TinyService.Validator.TinyServiceValidatorFactory factory)
        {
            this._servicecontroller = servicecontroller;
            this._factory = factory;
            Logger = ObjectFactory.GetService<ILoggerFactory>().Create(typeof(ManagerController));
            bus = ObjectFactory.GetService<IMessageBus>();
        }

        [Route("api/addblog")]
        [HttpGet]
        public async Task<IHttpActionResult> AddBlog()
        {
            BlogPostRequest req = new BlogPostRequest();
            req.Post = new BlogPost()
            {
                Tags = "双十一",
                Title = "博客",
                MetaTitle = "我是"
            };
            req.Post.BlogComments.Add(new BlogComment()
            {
                CommentText = "博客Text",
                CreatedOnUtc = DateTime.Now
            });
            var result = await this._servicecontroller.SendAsync<BlogPostRequest, Result>(req);
            var message = new AppMessage()
            {
                Name = "我是发布信息"
            };
            await bus.Publish<AppMessage>(message);
            return this.Ok(result);
        }


        [Route("api/query")]
        [HttpGet]
        public async Task<IHttpActionResult> QueryBlog()
        {


            var result = await this._servicecontroller.SendAsync<QueryBlogRequest, Result>(new QueryBlogRequest());
            return this.Ok(result);
        }

        [Route("api/Manager")]
        public async Task<IHttpActionResult> GetManagerList()
        {
            var result = await this._servicecontroller.SendAsync<QueryAllManager, IList<Manager>>(new QueryAllManager());
            return Ok(result);
        }
        [Route("api/Add")]
        [HttpGet]
        public async Task<IHttpActionResult> AddManager()
        {
            var manager = new AddManager()
              {
                  Body = new Manager()
                  {
                      Title = Guid.NewGuid().ToString("N")
                  }
              };

            var result = await this._servicecontroller.SendAsync<AddManager, Result>(manager);

            return this.Ok(new { result = result });
        }

        [Route("api/GetCount")]
        public async Task<IHttpActionResult> GetCount()
        {
            var count = await this._servicecontroller.SendAsync<CountManager, Result>(new CountManager());

            return this.Ok(new { Total = count });
        }
    }
}
