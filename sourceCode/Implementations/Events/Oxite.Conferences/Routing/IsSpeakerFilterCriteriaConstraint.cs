//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web;
using System.Web.Routing;
using Oxite.Modules.Conferences.Models;

namespace Oxite.Modules.Conferences.Routing
{
    public class IsSpeakerFilterCriteria : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return values[parameterName] as string != null && (new SpeakerFilterCriteria(values[parameterName].ToString())).HasCriteria();
        }
    }

    public class IsSpeaker : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            string val = values[parameterName] as string;
            return val != null && val.Contains("-");
        }

    }

}