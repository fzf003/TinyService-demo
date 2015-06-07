using CommonComposition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TinyService.Application
{

    [Component(IsSingleton = true)]
    public class Foo : IFoo
    {
        public Foo()
        {
            Console.WriteLine("开始"+Guid.NewGuid().ToString());
        }



        public void Dispose()
        {
            Console.WriteLine("结束" + Guid.NewGuid().ToString());
        }
    }

    public interface IFoo:IDisposable
    {

    }
}
