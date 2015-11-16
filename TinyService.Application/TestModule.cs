using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TinyService.Infrastructure;
using TinyService.Infrastructure.Proxy;
using TinyService.Modules;

namespace TinyService.Application
{
    [Component]
   
    public class TestModule:TinyModule
    {
        public override  void  Initialize()
        {
            
            Console.WriteLine("Module初始化.....");
        }
     }


   
}
