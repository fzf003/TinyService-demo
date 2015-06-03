using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyService.Infrastructure
{
    public interface IObjectContainer
    {
         TService Resolve<TService>() where TService : class;
    }
}
