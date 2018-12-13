//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.IO;
using System.Web;
using System.Web.Routing;

namespace Oxite.Modules.CMS.Routing
{
    public class IsPagePath : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            string pagePath = values[parameterName] != null ? values[parameterName].ToString() : "";

            //INFO: (erikpo) All paths without file extensions should be treated as pages. Those with file extensions should not be considered a match and fall through to IIS.
            return Path.GetExtension(pagePath) == "";
        }
    }
}
