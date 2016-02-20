using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyService.Caching
{
   
    public enum CacheExpirationMode
    {
         
        None,
        
        Duration,
         
        Sliding,
        
        Absolute
    }
}
