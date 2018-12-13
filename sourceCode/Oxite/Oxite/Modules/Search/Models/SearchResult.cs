//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using Oxite.Models;

namespace Oxite.Modules.Search.Models
{
    public class SearchResult : EntityBase, ISearchResult
    {
        public SearchResult(Guid id, string title, string body, string url, IDictionary<string, object> values)
            : base(id)
        {
            Title = title;
            Body = body;
            Url = url;
            Values = values;
        }

        #region ISearchResult Members

        public string Title { get; private set; }
        public string Body { get; private set; }
        public string Url { get; private set; }
        public IDictionary<string, object> Values { get; private set; }

        #endregion
    }
}
