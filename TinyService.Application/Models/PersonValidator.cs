using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Infrastructure;

namespace TinyService.Application.Models
{
    [Component]
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            this.RuleFor(customer => customer.Name).NotNull().WithMessage("不能为空！");

            this.RuleFor(customer => customer.Age)
                .GreaterThan(0)
                .WithMessage("ID不能为0");

        }
    }

}
