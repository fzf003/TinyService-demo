using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Infrastructure.CommonComposition;
using TinyService.Infrastructure.Proxy;


namespace TinyService.Application
{

    [Component(IsSingleton = false)]
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

        public void MySetup()
        {
            Console.WriteLine("MySetUp.....");
        }
    }

    [Interceptor(typeof(StringMethodInterceptor))]

    public interface IFoo
    {
        void MySetup();
    }
}
