using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIXVideos.Oxite.CachingRepositories;

namespace MIXVideos.Oxite.Test
{
    public class FakeCache : ICache
    {
        public List<FakeCacheEntry> Cache = new List<FakeCacheEntry>();

        #region ICache Members

        public void Add<T>(string key, T item, TimeSpan absoluteDuration)
        {
            Cache.Add(new FakeCacheEntry() { Duration = absoluteDuration, Item = item, Key = key });
        }

        public T Get<T>(string key)
        {
            FakeCacheEntry entry = Cache.Where(e => e.Key == key).FirstOrDefault();
            if (entry == null)
                return default(T);
            else
                return (T)(entry.Item);
        }

        #endregion

        public class FakeCacheEntry
        {
            public string Key { get; set; }
            public object Item { get; set; }
            public TimeSpan Duration { get; set; }
        }
    }
}
