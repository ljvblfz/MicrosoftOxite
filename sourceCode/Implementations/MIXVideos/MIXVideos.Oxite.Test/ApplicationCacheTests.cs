using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using System.Web.Caching;
using MIXVideos.Oxite.CachingRepositories;
using System.Web;

namespace MIXVideos.Oxite.Test
{
    public class ApplicationCacheTests
    {
        [Fact]
        public void AddStoresItem()
        {
            Cache cache = HttpRuntime.Cache;

            ApplicationCache appCache = new ApplicationCache(cache);

            string key = "key";
            string item = "item";

            appCache.Add(key, item, TimeSpan.FromMinutes(5));

            Assert.NotNull(cache[key]);
        }

        [Fact]
        public void GetReturnsItem()
        {
            Cache cache = HttpRuntime.Cache;

            ApplicationCache appCache = new ApplicationCache(cache);

            string key = "key";
            string item = "item";

            cache.Insert(key, item);

            string actual = appCache.Get<string>(key);

            Assert.Equal(item, actual);
        }

        [Fact]
        public void GetReturnsNullIfNotFound()
        {
            Cache cache = HttpRuntime.Cache;

            ApplicationCache appCache = new ApplicationCache(cache);

            string key = "key";
            cache.Remove(key);

            string actual = appCache.Get<string>(key);

            Assert.Null(actual);
        }

        [Fact]
        public void GetReturnsDefaultIfNotFoundValueType()
        {
            Cache cache = HttpRuntime.Cache;

            ApplicationCache appCache = new ApplicationCache(cache);

            string key = "key";
            cache.Remove(key);

            int actual = appCache.Get<int>(key);

            Assert.Equal(default(int), actual);
        }
    }
}
