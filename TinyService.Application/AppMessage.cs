using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 using TinyService.Infrastructure.Message;

namespace TinyService.Application
{
    public class ApplicationMessage:Message
    {
        public ApplicationMessage() : base() { }

        public string Name { get; set; }


    }
}
