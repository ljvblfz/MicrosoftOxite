//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web;
using System.Web.Routing;

namespace Oxite.Routing
{
    public class IsFolderPath : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            //TODO: (erikpo) Add a site setting for if the site owner wants anything that ends in slash to redirect or not

            return values[parameterName].ToString().EndsWith("/");
        }
    }
}
