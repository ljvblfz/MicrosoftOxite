//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Oxite.Infrastructure;
using Oxite.Modules.Bing.ModelBinders;
using Oxite.Modules.Bing.Services;
using Oxite.Modules.Search.Models;
using Oxite.Modules.Search.Services;
using Oxite.Routing;

namespace Oxite.Modules.Bing
{
    public class BingModule : IOxiteModule
    {
        private readonly IUnityContainer container;

        public BingModule(IUnityContainer container)
        {
            this.container = container;
        }

        #region IOxiteModule Members

        public void Initialize()
        {
        }

        public void Unload()
        {
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            string[] controllerNamespaces = new string[] { "Oxite.Modules.Search.Controllers" };

            routes.MapRoute(
                "PostsBySearch",
                "Search/{dataFormat}",
                new { controller = "Search", action = "List", dataFormat = "" },
                new { dataformat = "(|RSS|ATOM)" },
                controllerNamespaces
                );

            routes.MapRoute(
                "PageOfPostsBySearch",
                "Search/page{pageNumber}",
                new { controller = "Search", action = "List" },
                new { pageNumber = new IsInt() },
                controllerNamespaces
                );
        }

        public void RegisterCatchAllRoutes(RouteCollection routes)
        {
        }

        public void RegisterFilters(IFilterRegistry filterRegistry)
        {
            ControllerActionFilterCriteria listActionsCriteria = new ControllerActionFilterCriteria();
            listActionsCriteria.AddMethod<Oxite.Modules.Search.Controllers.SearchController>(p => p.List(null, 0, null));
            filterRegistry.Add(new[] { listActionsCriteria }, typeof(Oxite.Filters.PageSizeActionFilter));
        }

        public void RegisterModelBinders(ModelBinderDictionary modelBinders)
        {
            modelBinders[typeof(SearchCriteria)] = new SearchCriteriaModelBinder();
        }

        public void RegisterWithContainer()
        {
            container
                .RegisterType<ISearchResultService, BingSearchResultService>();
        }

        #endregion
    }
}
