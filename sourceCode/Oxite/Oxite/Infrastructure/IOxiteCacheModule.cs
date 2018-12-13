//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using Oxite.Infrastructure;
using Oxite.Models;

namespace Oxite.Infrastructure
{
    public interface IOxiteCacheModule : IOxiteModule
    {
        void Insert(string key, object value);
        T GetItems<T, K>(string key, Func<T> getUncachedItems, Func<K, IEnumerable<ICacheEntity>> getDependencies)
            where T : IEnumerable<K>;
        T GetItems<T, K>(string key, CachePartition partition, Func<T> getUncachedItems, Func<K, IEnumerable<ICacheEntity>> getDependencies)
            where T : IEnumerable<K>;
        T GetItem<T>(string key, Func<T> getUncachedItem, Func<T, IEnumerable<ICacheEntity>> getDependencies);
        void InvalidateItem(ICacheEntity item);
        void Invalidate<T>() where T : ICacheEntity;
        void Invalidate(string key);
    }
}
