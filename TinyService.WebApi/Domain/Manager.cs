using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TinyService.Domain.Entities;

namespace TinyService.WebApi.Domain
{
    public class Manager : Entity<string>
    {
        public Manager()
        {
            this.ID = Guid.NewGuid().ToString("N");
        }
        public string Title { get; set; }
    }
}