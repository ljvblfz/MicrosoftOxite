//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Oxite.Plugins
{
    public class PluginRoute : Route
    {
        public PluginRoute(Guid pluginID, string methodName, string url, RouteValueDictionary defaults, RouteValueDictionary constraints)
            : base(url, defaults, constraints, new MvcRouteHandler())
        {
            PluginID = pluginID;
            MethodName = methodName;
        }

        public Guid PluginID { get; private set; }
        public string MethodName { get; private set; }
    }
}
