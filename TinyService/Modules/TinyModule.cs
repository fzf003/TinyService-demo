using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Infrastructure;

namespace TinyService.Modules
{
    public abstract class TinyModule
    {
        public virtual void Initialize()
        {
           
        }
        public static bool IsModule(Type type)
        {
            return
                type.IsClass &&
                !type.IsAbstract &&
                typeof(TinyModule).IsAssignableFrom(type);
        }
    }
}
