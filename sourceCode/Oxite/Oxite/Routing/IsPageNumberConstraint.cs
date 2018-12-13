//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Routing;

namespace Oxite.Routing
{
    public class IsPageNumber : IRouteConstraint
    {
        private readonly Regex pageNumberRegex = new Regex(@"(?:(?<=^|/)Page(?<number>\d+)(?=$|/))?", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            string pageNumber = values[parameterName] as string;

            if (!string.IsNullOrEmpty(pageNumber))
                return pageNumberRegex.Match(pageNumber).Groups["number"].Success;

            return true;
        }
    }
}
