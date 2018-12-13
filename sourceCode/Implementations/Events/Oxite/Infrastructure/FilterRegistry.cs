//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Practices.Unity;

namespace Oxite.Infrastructure
{
    public class FilterRegistry : IFilterRegistry
    {
        private readonly IUnityContainer container;
        private readonly List<FilterRegistryItem> items;

        public FilterRegistry(IUnityContainer container)
        {
            this.container = container;
            items = new List<FilterRegistryItem>();
        }

        #region IFilterRegistry Members

        public void Clear()
        {
            items.Clear();
        }

        public void Add(IEnumerable<IFilterCriteria> filterCriteria, Type filterType)
        {
            items.Add(new FilterRegistryItem(filterCriteria, filterType));
        }

        public FilterInfo GetFilters(FilterRegistryContext context)
        {
            FilterInfo filters = new FilterInfo();

            foreach (FilterRegistryItem item in items)
            {
                if (item.Match(context))
                {
                    object filterObject = container.Resolve(item.FilterType);

                    if (filterObject is IActionFilter)
                        filters.ActionFilters.Add((IActionFilter)filterObject);

                    if (filterObject is IAuthorizationFilter)
                        filters.AuthorizationFilters.Add((IAuthorizationFilter)filterObject);

                    //if (filterObject is IExceptionFilter)
                    //    filters.ExceptionFilters.Add((IExceptionFilter)filterObject);

                    if (filterObject is IResultFilter)
                        filters.ResultFilters.Add((IResultFilter)filterObject);
                }
            }

            return filters;
        }

        #endregion
    }
}
