//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;

namespace Oxite.Modules.Search.Models
{
    public interface ISearchResult
    {
        string Title { get; }
        string Body { get; }
        string Url { get; }
        IDictionary<string, object> Values { get; }
    }
}
