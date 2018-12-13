//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Routing;

namespace Oxite.Routing
{
    public class OxiteRoute
    {
        public OxiteRoute(string name, string url)
            : this(name, url, (RouteValueDictionary)null)
        {
        }

        public OxiteRoute(string name, string url, object defaults)
            : this(name, url, defaults, (object)null)
        {
        }

        public OxiteRoute(string name, string url, object defaults, object constraints)
            : this(name, url, defaults, constraints, (string[])null)
        {
        }

        public OxiteRoute(string name, string url, object defaults, object constraints, string[] controllerNamespaces)
        {
            Name = name;
            Url = url;
            Defaults = defaults;
            Constraints = constraints;
            ControllerNamespaces = controllerNamespaces;
        }

        public string Name { get; private set; }
        public string Url { get; private set; }
        public object Defaults { get; private set; }
        public object Constraints { get; private set; }
        public string[] ControllerNamespaces { get; private set; }
    }
}
