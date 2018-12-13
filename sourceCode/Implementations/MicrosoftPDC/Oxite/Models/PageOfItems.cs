//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;

namespace Oxite.Models
{
    public class PageOfItems<T> : List<T>, IPageOfItems<T>
    {
        public PageOfItems(IEnumerable<T> items, int pageIndex, int pageSize, int totalItemCount)
        {
            AddRange(items);
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalItemCount = totalItemCount;
            TotalPageCount = (int)Math.Ceiling(totalItemCount / (double)pageSize);        }

        #region IPageOfItems<T> Members

        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int TotalItemCount { get; private set; }
        public int TotalPageCount { get; private set; }

        #endregion
    }
}
