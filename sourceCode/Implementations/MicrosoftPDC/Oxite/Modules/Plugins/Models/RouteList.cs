//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;

namespace Oxite.Modules.Plugins.Models
{
    public class RouteList : List<Route>
    {
        public void Add(string methodName, string url)
        {
            Add(methodName, url, null);
        }

        public void Add(string methodName, string url, object defaults)
        {
            Add(methodName, url, defaults, null);
        }

        public void Add(string methodName, string url, object defaults, object constraints)
        {
            Add(new Route(methodName, url, defaults, constraints));
        }
    }
}
