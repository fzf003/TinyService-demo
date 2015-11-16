using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using TinyService.Infrastructure;

namespace TinyService.Caching
{
      [Component(IsSingleton = true)]
    public class MemoryCacheProvider : ICacheProvider
    {
        private const string _tagKey = "global::tag::{0}";

       
        public bool Add(CacheKey cacheKey, object value, CachePolicy cachePolicy)
        {
            string key = GetKey(cacheKey);
            var item = new CacheItem(key, value);
            var policy = CreatePolicy(cacheKey, cachePolicy);

            var existing = MemoryCache.Default.AddOrGetExisting(item, policy);
            return existing.Value == null;
        }

      
        public object Get(CacheKey cacheKey)
        {
            string key = GetKey(cacheKey);
            return MemoryCache.Default.Get(key);
        }

      
        public object GetOrAdd(CacheKey cacheKey, Func<CacheKey, object> valueFactory, CachePolicy cachePolicy)
        {

            var key = GetKey(cacheKey);
            var cachedResult = MemoryCache.Default.Get(key);

            if (cachedResult != null)
            {
                //Debug.WriteLine("Cache Hit : " + key);
                return cachedResult;
            }

            //Debug.WriteLine("Cache Miss: " + key);

           
            var value = valueFactory(cacheKey);
            this.Add(cacheKey, value, cachePolicy);

            return value;
        }

#if NET45
      
        public async Task<object> GetOrAddAsync(CacheKey cacheKey, Func<CacheKey, Task<object>> valueFactory, CachePolicy cachePolicy)
        {
            var key = GetKey(cacheKey);
            var cachedResult = MemoryCache.Default.Get(key);

            if (cachedResult != null)
            {
                Debug.WriteLine("Cache Hit : " + key);
                return cachedResult;
            }

            Debug.WriteLine("Cache Miss: " + key);

            // get value and add to cache, not bothered
            // if it succeeds or not just rerturn the value
            var value = await valueFactory(cacheKey).ConfigureAwait(false);
            this.Add(cacheKey, value, cachePolicy);

            return value;
        }
#endif

     
        public object Remove(CacheKey cacheKey)
        {
            string key = GetKey(cacheKey);
            return MemoryCache.Default.Remove(key);
        }

        
        public int Expire(CacheTag cacheTag)
        {
            string key = GetTagKey(cacheTag);
            var item = new CacheItem(key, DateTimeOffset.UtcNow.Ticks);
            var policy = new CacheItemPolicy { AbsoluteExpiration = ObjectCache.InfiniteAbsoluteExpiration };

            MemoryCache.Default.Set(item, policy);
            return 0;
        }

    
        public bool Set(CacheKey cacheKey, object value, CachePolicy cachePolicy)
        {
            string key = GetKey(cacheKey);
            var item = new CacheItem(key, value);
            var policy = CreatePolicy(cacheKey, cachePolicy);

            MemoryCache.Default.Set(item, policy);
            return true;
        }

     
        public long ClearCache()
        {
            return MemoryCache.Default.Trim(100);
        }


       
        internal static string GetKey(CacheKey cacheKey)
        {
            return cacheKey.Key;
        }

        internal static string GetTagKey(CacheTag t)
        {
            return string.Format(_tagKey, t);
        }

        internal static CacheItemPolicy CreatePolicy(CacheKey key, CachePolicy cachePolicy)
        {
            var policy = new CacheItemPolicy();

            switch (cachePolicy.Mode)
            {
                case CacheExpirationMode.Sliding:
                    policy.SlidingExpiration = cachePolicy.SlidingExpiration;
                    break;
                case CacheExpirationMode.Absolute:
                    policy.AbsoluteExpiration = cachePolicy.AbsoluteExpiration;
                    break;
                case CacheExpirationMode.Duration:
                    policy.AbsoluteExpiration = DateTimeOffset.Now.Add(cachePolicy.Duration);
                    break;
                default:
                    policy.AbsoluteExpiration = ObjectCache.InfiniteAbsoluteExpiration;
                    break;
            }

            var changeMonitor = CreateChangeMonitor(key);
            if (changeMonitor != null)
                policy.ChangeMonitors.Add(changeMonitor);

            return policy;
        }

        internal static CacheEntryChangeMonitor CreateChangeMonitor(CacheKey key)
        {
            var cache = MemoryCache.Default;

            var tags = key.Tags
                .Select(GetTagKey)
                .ToList();

            if (tags.Count == 0)
                return null;

             foreach (string tag in tags)
                cache.AddOrGetExisting(tag, DateTimeOffset.UtcNow.Ticks, ObjectCache.InfiniteAbsoluteExpiration);

            return cache.CreateCacheEntryChangeMonitor(tags);
        }
    }
}
