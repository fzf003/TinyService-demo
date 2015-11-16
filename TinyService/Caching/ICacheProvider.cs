using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyService.Caching
{
    public interface ICacheProvider
    {
        
        bool Add(CacheKey cacheKey, object value, CachePolicy cachePolicy);

         object Get(CacheKey cacheKey);

         object GetOrAdd(CacheKey cacheKey, Func<CacheKey, object> valueFactory, CachePolicy cachePolicy);

#if NET45
         Task<object> GetOrAddAsync(CacheKey cacheKey, Func<CacheKey, Task<object>> valueFactory, CachePolicy cachePolicy);
#endif
         object Remove(CacheKey cacheKey);
         int Expire(CacheTag cacheTag);

         bool Set(CacheKey cacheKey, object value, CachePolicy cachePolicy);

         long ClearCache();
    }
}
