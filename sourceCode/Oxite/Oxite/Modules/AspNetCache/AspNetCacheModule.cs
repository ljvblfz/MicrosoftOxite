//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Oxite.Infrastructure;
using Oxite.Models;

namespace Oxite.Modules.AspNetCache
{
    public class AspNetCacheModule : IOxiteCacheModule
    {
        private readonly IUnityContainer container;

        public AspNetCacheModule(IUnityContainer container)
        {
            this.container = container;
        }

        #region IOxiteModule Members

        public void Initialize()
        {
        }

        public void Unload()
        {
        }

        public void RegisterRoutes(RouteCollection routes)
        {
        }

        public void RegisterCatchAllRoutes(RouteCollection routes)
        {
        }

        public void RegisterFilters(IFilterRegistry filterRegistry)
        {
        }

        public void RegisterModelBinders(ModelBinderDictionary modelBinders)
        {
        }

        public void RegisterWithContainer()
        {
            container
                .RegisterType<IOxiteCacheModule, AspNetCacheModule>();
        }

        #endregion

        #region IOxiteCacheModule Members

        public void Insert(string key, object value)
        {
            insert(HttpContext.Current, key, value);
        }

        public T GetItems<T, K>(string key, Func<T> getUncachedItems, Func<K, IEnumerable<ICacheEntity>> getDependencies)
            where T : IEnumerable<K>
        {
            return GetItems<T, K>(key, null, getUncachedItems, getDependencies);
        }

        public T GetItems<T, K>(string key, CachePartition partition, Func<T> getUncachedItems, Func<K, IEnumerable<ICacheEntity>> getDependencies)
            where T : IEnumerable<K>
        {
            HttpContext context = HttpContext.Current;
            string fullKey = partition != null ? key + partition.ToString() : key;
            T cacheItem = (T)context.Cache[fullKey];

            if (cacheItem != null) return cacheItem;

            T items = getUncachedItems();

            if (items != null)
            {
                insert(context, fullKey, items);

                // partitions
                if (partition != null)
                {
                    CacheDependencyList partitionList = context.Cache[key] as CacheDependencyList;

                    if (partitionList == null)
                        insert(context, key, partitionList = new CacheDependencyList());

                    string keyAndPartition = key + partition.ToString();

                    if (!partitionList.Contains(keyAndPartition))
                        partitionList.Add(keyAndPartition);
                }

                // dependencies
                if (getDependencies != null)
                    if (items.Count() > 0)
                        foreach (K item in items)
                            // add dependencies for each item
                            foreach (ICacheEntity dependency in getDependencies(item))
                                cacheDependency(key, context, dependency);
                    else
                        // allow the caller to add dependencies for dependent objects even though no items were found
                        foreach (ICacheEntity dependency in getDependencies(default(K)))
                            cacheDependency(key, context, dependency);
            }

            return items;
        }

        public T GetItem<T>(string key, Func<T> getUncachedItem, Func<T, IEnumerable<ICacheEntity>> getDependencies)
        {
            HttpContext context = HttpContext.Current;
            T cacheItem = (T)context.Cache[key];

            if (cacheItem != null) return cacheItem;

            T item = getUncachedItem();

            if (item != null)
            {
                insert(context, key, item);

                if (getDependencies != null)
                    foreach (ICacheEntity dependency in getDependencies(item))
                        cacheDependency(key, context, dependency);
            }

            return item;
        }

        public void InvalidateItem(ICacheEntity item)
        {
            HttpContext context = HttpContext.Current;

            if (item is ICacheEntity)
                invalidateDependency(context, ((ICacheEntity)item).GetCacheItemKey());

            foreach (ICacheEntity cacheItem in item.GetCacheDependencyItems())
                invalidateDependency(context, cacheItem.GetCacheItemKey());
        }

        public void Invalidate<T>() where T : ICacheEntity
        {
            HttpContext context = HttpContext.Current;
            CacheDependencyList dependencyList = context.Cache[typeof(T).Name] as CacheDependencyList;

            if (dependencyList != null)
                foreach (string dependency in dependencyList)
                    invalidateDependency(context, dependency);
        }

        public void Invalidate(string key)
        {
            invalidateByKey(HttpContext.Current, key);
        }

        #endregion

        #region Private Methods

        private static void insert(HttpContext context, string key, object value)
        {
            //TODO: (erikpo) Once moved to a caching module, change the hardcoded sliding expiration TimeSpan to come from a value in the web.config
            context.Cache.Insert(key, value, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(30));
        }

        private static void invalidateByKey(HttpContext context, string key)
        {
            context.Cache.Remove(key);
        }

        private static void invalidateDependency(HttpContext context, string dependencyKey)
        {
            CacheDependencyList dependencyList = context.Cache[dependencyKey] as CacheDependencyList;

            if (dependencyList != null)
            {
                foreach (string dependency in dependencyList)
                {
                    CacheDependencyList dependencyItems = context.Cache[dependency] as CacheDependencyList;

                    if (dependencyItems != null)
                        foreach (string dependencyItem in dependencyItems)
                            invalidateByKey(context, dependencyItem);

                    invalidateByKey(context, dependency);
                }

                invalidateByKey(context, dependencyKey);
            }
        }

        private static void cacheDependency(string key, HttpContext context, ICacheEntity item)
        {
            string dependencyKey = item.GetCacheItemKey();
            CacheDependencyList dependencyList = context.Cache[dependencyKey] as CacheDependencyList;

            if (dependencyList == null)
                insert(context, dependencyKey, dependencyList = new CacheDependencyList());

            if (!dependencyList.Contains(key))
                dependencyList.Add(key);

            string typeDependencyKey = item.GetType().Name;
            CacheDependencyList typeDependencyList = context.Cache[typeDependencyKey] as CacheDependencyList;

            if (typeDependencyList == null)
                insert(context, typeDependencyKey, typeDependencyList = new CacheDependencyList());

            if (!typeDependencyList.Contains(dependencyKey))
                typeDependencyList.Add(dependencyKey);
        }

        #endregion
    }
}
