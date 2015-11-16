using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Infrastructure;
using TinyService.Infrastructure.Proxy;


namespace TinyService.Application
{

    [Component(IsSingleton = false)]
    public class Foo : IFoo
    {

        public Foo()
        {
        }

        public void Dispose()
        {
        }
     
        public void MySetup()
        {
            Console.WriteLine("MySetUp.....");
        }

        public string YouSetUP()
        {

            return DateTime.Now.ToString();
        }
    }

    [Interceptor(typeof(StringMethodInterceptor))]
    [Interceptor(typeof(AuthorizationInterceptor))]
    public interface IFoo
    {
        void MySetup();
        string YouSetUP();
    }
}
