using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TinyService.Infrastructure.Message;
using TinyService.Infrastructure.RequestHandler;

namespace TinyService.WebApi.Domain
{
    public class Result
    {
       public bool IsSuccess { get; set; }

       public int Count { get; set; }
    }

    public class AddManager:Message,IRequest
    {
        public Manager Body { get; set; }
    }

    public class QueryAllManager:IRequest
    {

    }

    public class CountManager:IRequest
    {

    }

}