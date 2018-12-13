//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using System.Web.Routing;

namespace Oxite.Modules.Plugins.Models
{
    public class Route
    {
        public Route(string methodName, string url, object defaults, object constraints)
        {
            MethodName = methodName;
            Url = url;
            Defaults = new RouteValueDictionary(defaults);
            Constraints = new RouteValueDictionary(constraints);
        }

        public string MethodName { get; private set; }
        public string Url { get; private set; }
        public RouteValueDictionary Defaults { get; private set; }
        public RouteValueDictionary Constraints { get; private set; }

        public RouteValueDictionary MergeWithDefaults(object values)
        {
            return MergeWithDefaults(new RouteValueDictionary(values));
        }

        public RouteValueDictionary MergeWithDefaults(RouteValueDictionary dictionary)
        {
            RouteValueDictionary defaults = merge(Defaults, dictionary);

            if (defaults.Count == 0)
                return null;

            return defaults;
        }

        public RouteValueDictionary MergeWithConstraints(object values)
        {
            return MergeWithConstraints(new RouteValueDictionary(values));
        }

        public RouteValueDictionary MergeWithConstraints(RouteValueDictionary dictionary)
        {
            RouteValueDictionary constraints = merge(Constraints, dictionary);

            if (constraints.Count == 0)
                return null;

            return constraints;
        }

        private static RouteValueDictionary merge(RouteValueDictionary dictionary1, RouteValueDictionary dictionary2)
        {
            if (dictionary2 != null)
                foreach (KeyValuePair<string, object> item in dictionary2)
                    dictionary1[item.Key] = item.Value;

            return dictionary1;
        }
    }
}
