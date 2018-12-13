using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIXVideos.Oxite.CachingRepositories
{
    public interface ICache
    {
        void Add<T>(string key, T item, TimeSpan absoluteDuration);
        T Get<T>(string key);
    }
}
