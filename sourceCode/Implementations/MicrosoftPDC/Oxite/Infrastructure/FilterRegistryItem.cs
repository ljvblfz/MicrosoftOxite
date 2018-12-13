//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;

namespace Oxite.Infrastructure
{
    public class FilterRegistryItem
    {
        private readonly List<IFilterCriteria> filterCriteria;
        public Type FilterType { get; private set; }
        
        public FilterRegistryItem(IEnumerable<IFilterCriteria> filterCriteria, Type filterType)
        {
            this.filterCriteria = new List<IFilterCriteria>(filterCriteria);
            FilterType = filterType;
        }

        public bool Match(FilterRegistryContext context)
        {
            return filterCriteria.Aggregate(true, (prev, f) => prev ? f.Match(context) : prev);
        }
    }
}
