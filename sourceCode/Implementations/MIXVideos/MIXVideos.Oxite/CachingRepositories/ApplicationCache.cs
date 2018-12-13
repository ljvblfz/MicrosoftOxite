using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MIXVideos.Oxite.CachingRepositories
{
    public class ApplicationCache : ICache
    {
        System.Web.Caching.Cache cache;

        public ApplicationCache(System.Web.Caching.Cache cache)
        {
            this.cache = cache;
        }

        #region ICache Members

        public void Add<T>(string key, T item, TimeSpan absoluteDuration)
        {
            cache.Insert(key, item, null, DateTime.UtcNow.Add(absoluteDuration), System.Web.Caching.Cache.NoSlidingExpiration);
        }

        public T Get<T>(string key)
        {
            object item = cache[key];
            if (item == null)
                return default(T);
            else
                return (T)item;
        }

        #endregion
    }
}
