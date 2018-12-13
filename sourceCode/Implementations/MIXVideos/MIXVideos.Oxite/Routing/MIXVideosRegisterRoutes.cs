//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using MIXVideos.Oxite.Services;
using Oxite.Infrastructure;
using Oxite.Routing;
using Oxite.Services;

namespace MIXVideos.Oxite.Routing
{
    public class MIXVideosRegisterRoutes : IRegisterRoutes
    {
        private readonly IAreaService areaService;
        private readonly IPostViewService postViewService;
        private readonly IRouteModifier routeModifier;

        public MIXVideosRegisterRoutes(IAreaService areaService, IPostViewService postViewService, IRouteModifier routeModifier)
        {
            this.areaService = areaService;
            this.postViewService = postViewService;
            this.routeModifier = routeModifier;
        }

        #region IRegisterRoutes Members

        public void RegisterRoutes(RouteCollection routes)
        {
            string areasConstraint = createConstraint(areaService.GetAreas().Select(a => a.Name));
            string viewTypesConstraint = createConstraint(postViewService.GetViewTypes());
            string[] controllerNamespaces = new string[] { "MIXVideos.Oxite.Controllers" };

            routes.MapRoute(
                routeModifier,
                "PostsAll",
                "MIX09/All",
                new { controller = "Post2", action = "ListAll", areaName = "MIX09" },
                null,
                controllerNamespaces
                );

            routes.MapRoute(
                routeModifier,
                "PostView",
                "{areaName}/{slug}/View/{viewType}",
                new { controller = "PostView", action = "Item" },
                new { areaName = areasConstraint, viewType = viewTypesConstraint },
                controllerNamespaces
                );

            routes.Add("OldSessionsForSearch", new Route("", null, new RouteValueDictionary(new { selectedSearch = new IsOldStyleSearchConstraint()}), new SearchRedirectRouteHandler()));

            routes.MapRoute(
                routeModifier,
                "MIX09FileFeed",
                "MIX09/Feeds/{typeName}/RSS",
                new { controller = "Feed", action = "List", dataFormat = "RSS" },
                new { typeName = "(WMV|WMVHigh|MP4|WMA|MP3)" },
                controllerNamespaces
                );
        }

        #endregion

        private static string createConstraint(IEnumerable<string> items)
        {
            return
                items != null && items.Count() > 0
                    ? items.Count() > 1
                        ? string.Format("({0})", string.Join("|", items.ToArray())) : items.ElementAt(0)
                    : "";
        }
    }
}
