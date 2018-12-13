//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Oxite.Modules.Conferences.Services;

namespace Oxite.Modules.Conferences.Routing
{
    public class EventConstraint : IRouteConstraint
    {
        private readonly IUnityContainer container;

        public EventConstraint(IUnityContainer container)
        {
            this.container = container;
        }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            IEventService eventService = container.Resolve<IEventService>();
            string eventName = values[parameterName].ToString();

            return eventService.GetEventExists(eventName);
        }
    }
}
