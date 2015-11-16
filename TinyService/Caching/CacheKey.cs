using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyService.Caching
{

    public class CacheKey
    {
        private readonly string _key;
        private readonly HashSet<CacheTag> _tags;

         public CacheKey(string key)
            : this(key, Enumerable.Empty<string>())
        { }

       
        public CacheKey(string key, IEnumerable<string> tags)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (tags == null)
                throw new ArgumentNullException("tags");

            _key = key;

            var cacheTags = tags.Select(k => new CacheTag(k));
            _tags = new HashSet<CacheTag>(cacheTags);
        }

 
        public string Key
        {
            get { return _key; }
        }

    
        public HashSet<CacheTag> Tags
        {
            get { return _tags; }
        }
    }
}
