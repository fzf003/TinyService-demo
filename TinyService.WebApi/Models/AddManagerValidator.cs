using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TinyService.Infrastructure;
using TinyService.WebApi.Domain;
using TinyService.WebApi.Models.Request;

namespace TinyService.WebApi.Models
{

    [Component]
    public class AddManagerValidator : AbstractValidator<AddManager>
    {
        public AddManagerValidator()
        {
            this.RuleFor(add => add.Body).SetValidator(new ManagerValidator());
        }
    }


    [Component]
    public class ManagerValidator:AbstractValidator<Manager>
    {
        public ManagerValidator()
        {
            this.RuleFor(p => p.ID).NotEmpty().WithMessage("ID不能为空");
            this.RuleFor(p => p.Title).NotEmpty().WithMessage("Title不能为空");
        }
    }


   [Component]
    public class BlogPostRequestValidator:AbstractValidator<BlogPostRequest>
    {
         public BlogPostRequestValidator()
         {
             //this.RuleFor(p => p.Post).SetValidator(new BlogCommentValidator());
         }
    }

    public class BlogCommentValidator:AbstractValidator<BlogComment>
    {
        public BlogCommentValidator()
        {
            this.RuleFor(p => p.CustomerId).GreaterThan(0)
                .WithMessage("不能为0");
            this.RuleFor(p => p.BlogPostId).GreaterThan(0)
                .WithMessage("不能为0");
        }
    }
}