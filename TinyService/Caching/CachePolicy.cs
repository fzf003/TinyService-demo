using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace TinyService.Caching
{
    public class CachePolicy
    {
        private static readonly Lazy<CachePolicy> _current = new Lazy<CachePolicy>(() => new CachePolicy());

       
        public static CachePolicy Default
        {
            get { return _current.Value; }
        }

       
        public CachePolicy()
        {
            Mode = CacheExpirationMode.None;
            _absoluteExpiration = ObjectCache.InfiniteAbsoluteExpiration;
            _slidingExpiration = ObjectCache.NoSlidingExpiration;
            _duration = TimeSpan.Zero;
        }

    
        public CacheExpirationMode Mode { get; set; }

        private DateTimeOffset _absoluteExpiration;

     
        public DateTimeOffset AbsoluteExpiration
        {
            get { return _absoluteExpiration; }
            set
            {
                _absoluteExpiration = value;
                Mode = CacheExpirationMode.Absolute;
            }
        }

        private TimeSpan _slidingExpiration;

      
        public TimeSpan SlidingExpiration
        {
            get { return _slidingExpiration; }
            set
            {
                _slidingExpiration = value;
                Mode = CacheExpirationMode.Sliding;
            }
        }

        private TimeSpan _duration;

      
        public TimeSpan Duration
        {
            get { return _duration; }
            set
            {
                _duration = value;
                Mode = CacheExpirationMode.Duration;
            }
        }

         public static CachePolicy WithDurationExpiration(TimeSpan expirationSpan)
        {
            var policy = new CachePolicy
            {
                Duration = expirationSpan
            };

            return policy;
        }

         public static CachePolicy WithAbsoluteExpiration(DateTimeOffset absoluteExpiration)
        {
            var policy = new CachePolicy
            {
                AbsoluteExpiration = absoluteExpiration
            };

            return policy;
        }
         public static CachePolicy WithSlidingExpiration(TimeSpan slidingExpiration)
        {
            var policy = new CachePolicy
            {
                SlidingExpiration = slidingExpiration
            };

            return policy;
        }

    }
}
