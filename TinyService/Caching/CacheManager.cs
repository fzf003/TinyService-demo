using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Infrastructure;

namespace TinyService.Caching
{
    public class CacheManager
    {
        private static readonly Lazy<CacheManager> _current = new Lazy<CacheManager>(() => new CacheManager());

     
        public static CacheManager Current
        {
            get { return _current.Value; }
        }

         public bool Add(string key, object value)
        {
            var cachePolicy = new CachePolicy();
            return Add(key, value, cachePolicy);
        }

        public bool Add(string key, object value, DateTimeOffset absoluteExpiration)
        {
            var cachePolicy = CachePolicy.WithAbsoluteExpiration(absoluteExpiration);
            return Add(key, value, cachePolicy);
        }

         public bool Add(string key, object value, TimeSpan slidingExpiration)
        {
            var cachePolicy = CachePolicy.WithSlidingExpiration(slidingExpiration);
            return Add(key, value, cachePolicy);
        }

         public bool Add(string key, object value, CachePolicy cachePolicy)
        {
            var cacheKey = new CacheKey(key);
            return Add(cacheKey, value, cachePolicy);
        }

         public virtual bool Add(CacheKey cacheKey, object value, CachePolicy cachePolicy)
        {
            var provider = ResolveProvider();
            return provider.Add(cacheKey, value, cachePolicy);
        }


         public virtual object Get(string key)
        {
            var cacheKey = new CacheKey(key);

            var provider = ResolveProvider();
            var item = provider.Get(cacheKey);

            return item;
        }


      
        public object GetOrAdd(string key, object value)
        {
            var policy = new CachePolicy();
            return GetOrAdd(key, value, policy);
        }

     
        public object GetOrAdd(string key, object value, DateTimeOffset absoluteExpiration)
        {
            var policy = CachePolicy.WithAbsoluteExpiration(absoluteExpiration);
            return GetOrAdd(key, value, policy);
        }

     
        public object GetOrAdd(string key, object value, TimeSpan slidingExpiration)
        {
            var policy = CachePolicy.WithSlidingExpiration(slidingExpiration);
            return GetOrAdd(key, value, policy);
        }

       
        public object GetOrAdd(string key, object value, CachePolicy cachePolicy)
        {
            var cacheKey = new CacheKey(key);
            return GetOrAdd(cacheKey, value, cachePolicy);
        }

        
        public object GetOrAdd(CacheKey key, object value, CachePolicy cachePolicy)
        {
            return GetOrAdd(key, k => value, cachePolicy);
        }


        
        public object GetOrAdd(string key, Func<string, object> valueFactory)
        {
            return GetOrAdd(key, valueFactory, new CachePolicy());
        }

        
        public object GetOrAdd(string key, Func<string, object> valueFactory, DateTimeOffset absoluteExpiration)
        {
            var policy = CachePolicy.WithAbsoluteExpiration(absoluteExpiration);
            return GetOrAdd(key, valueFactory, policy);
        }

        
        public object GetOrAdd(string key, Func<string, object> valueFactory, TimeSpan slidingExpiration)
        {
            var policy = CachePolicy.WithSlidingExpiration(slidingExpiration);
            return GetOrAdd(key, valueFactory, policy);
        }

     
        public object GetOrAdd(string key, Func<string, object> valueFactory, CachePolicy cachePolicy)
        {
            var cacheKey = new CacheKey(key);
            return GetOrAdd(cacheKey, valueFactory, cachePolicy);
        }

        
        public virtual object GetOrAdd(CacheKey cacheKey, Func<CacheKey, object> valueFactory, CachePolicy cachePolicy)
        {
            var provider = ResolveProvider();
            var item = provider.GetOrAdd(cacheKey, valueFactory, cachePolicy);

            return item;
        }

#if NET45
        /// <summary>
        /// Gets the cache value for the specified key that is already in the dictionary or the new value for the key as returned asynchronously by <paramref name="valueFactory"/>.
        /// </summary>
        /// <param name="cacheKey">A unique identifier for the cache entry.</param>
        /// <param name="valueFactory">The asynchronous function used to generate a value to insert into cache.</param>
        /// <param name="cachePolicy">An object that contains eviction details for the cache entry.</param>
        /// <returns>
        /// The value for the key. This will be either the existing value for the key if the key is already in the dictionary, 
        /// or the new value for the key as returned by <paramref name="valueFactory"/> if the key was not in the dictionary.
        /// </returns>
        public virtual Task<object> GetOrAddAsync(CacheKey cacheKey, Func<CacheKey, Task<object>> valueFactory, CachePolicy cachePolicy)
        {
            var provider = ResolveProvider();
            var item = provider.GetOrAddAsync(cacheKey, valueFactory, cachePolicy);

            return item;
        }
#endif

         public object Remove(string key)
        {
            var cacheKey = new CacheKey(key);
            return Remove(cacheKey);
        }

         public virtual object Remove(CacheKey cacheKey)
        {
            var provider = ResolveProvider();
            var item = provider.Remove(cacheKey);

            return item;
        }


         public int Expire(string tag)
        {
            var cacheTag = new CacheTag(tag);
            return Expire(cacheTag);
        }

         public virtual int Expire(CacheTag cacheTag)
        {
            var provider = ResolveProvider();
            var item = provider.Expire(cacheTag);

            return item;
        }


         public void Set(string key, object value)
        {
            var policy = new CachePolicy();
            Set(key, value, policy);
        }

         public void Set(string key, object value, DateTimeOffset absoluteExpiration)
        {
            var policy = CachePolicy.WithAbsoluteExpiration(absoluteExpiration);
            Set(key, value, policy);
        }

         public void Set(string key, object value, TimeSpan slidingExpiration)
        {
            var policy = CachePolicy.WithSlidingExpiration(slidingExpiration);
            Set(key, value, policy);
        }

         public void Set(string key, object value, CachePolicy cachePolicy)
        {
            var cacheKey = new CacheKey(key);
            Set(cacheKey, value, cachePolicy);
        }

          public virtual void Set(CacheKey cacheKey, object value, CachePolicy cachePolicy)
        {
            var provider = ResolveProvider();
            provider.Set(cacheKey, value, cachePolicy);
        }

    
        public virtual void Clear()
        {
            var provider = ResolveProvider();
            provider.ClearCache();
        }


        protected ICacheProvider ResolveProvider()
        {

            var provider = ObjectFactory.GetService<ICacheProvider>();
            if (provider == null)
                throw new InvalidOperationException("Could not resolve the ICacheProvider. Make sure ICacheProvider is registered in the Locator.Current container.");

            return provider;
        }
    }
}
