using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyService.Application.Models
{
    [ValidatorAttribute(typeof(PersonValidator))]
    public class Person
    {
        public string Name { get; set; }

        public int Age { get; set; }
    }
}
