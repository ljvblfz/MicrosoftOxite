//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web;
using System.Web.Routing;

namespace Oxite.Routing
{
    public class IsInFormCollection : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            var paramValues = httpContext.Request.Form.GetValues(parameterName);
            return paramValues != null && paramValues.Length > 0;
        }
    }
}