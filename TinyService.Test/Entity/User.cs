using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Domain.Entities;

namespace TinyService.Test.Entity
{
    public class User:AggregateRoot<string>
    {
        public override string Id
        {
            get
            {
                return Guid.NewGuid().ToString("N");
            }
        }
        public string Name { get; set; }

        public int Age { get; set; }

        public User(string name,int age)
        {
            this.Name = name;
            this.Age = age;
         }

        public void ChangeName(string _name)
        {
            this.Name = _name;
        }
    }
}
