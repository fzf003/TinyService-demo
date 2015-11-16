using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TinyService.Infrastructure.Message;
using TinyService.Infrastructure.RequestHandler;

namespace TinyService.WebApi.Models.Request
{
    [ValidatorAttribute(typeof(BlogPostRequestValidator))]
    public class BlogPostRequest : Message, IRequest
    {
        public BlogPost Post { get; set; }

    }

    public class QueryBlogRequest : IRequest
    {

    }

}