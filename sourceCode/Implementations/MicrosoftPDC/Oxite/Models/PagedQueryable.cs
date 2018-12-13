//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Oxite.Services;

namespace Oxite.Models
{
    public class PagedQueryable<T> : IQueryable<T>
    {
        private readonly IQueryable<T> potentialList;
        private Dictionary<PageID, IPageOfItems<T>> pagedLists;

        public PagedQueryable(IQueryable<T> potentialList)
        {
            this.potentialList = potentialList;
            pagedLists = new Dictionary<PageID, IPageOfItems<T>>(5);
        }

        public IPageOfItems<T> GetPage(int pageIndex, int pageSize)
        {
            PageID key = new PageID(pageIndex, pageSize);
            IPageOfItems<T> pagedList = pagedLists.ContainsKey(key) ? pagedLists[key] : null;

            if (pagedList == null)
            {
                pagedList = potentialList.GetPage(key.PageIndex, key.PageSize);
                pagedLists.Add(key, pagedList);
            }

            return pagedList;
        }

        #region IQueryable Members

        public Type ElementType
        {
            get { return potentialList.ElementType; }
        }

        public Expression Expression
        {
            get { return potentialList.Expression; }
        }

        public IQueryProvider Provider
        {
            get { return potentialList.Provider; }
        }

        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return potentialList.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return potentialList.GetEnumerator();
        }

        #endregion

        #region Class PageID

        private class PageID : IEquatable<PageID>
        {
            private int pageIndex;
            private int pageSize;

            public PageID(int pageIndex, int pageSize)
            {
                this.pageIndex = pageIndex;
                this.pageSize = pageSize;
            }

            public int PageIndex
            {
                get { return pageIndex; }
            }

            public int PageSize
            {
                get { return pageSize; }
            }

            #region IEquatable<PageID> Members

            public bool Equals(PageID other)
            {
                return this.PageIndex == other.PageIndex && this.PageSize == other.PageSize;
            }

            #endregion
        }

        #endregion
    }
}
