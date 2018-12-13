//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Oxite.Infrastructure;
using Oxite.ModelBinders;
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
                "Search/{pageNumber}/{dataFormat}",
                new { controller = "Search", action = "List", pageNumber = "", dataFormat = "" },
                new { pageNumber = new IsPageNumber(), dataformat = "(|RSS|ATOM)" },
                controllerNamespaces
                );
        }

        public void RegisterCatchAllRoutes(RouteCollection routes)
        {
        }

        public void RegisterFilters(IFilterRegistry filterRegistry)
        {
        }

        public void RegisterModelBinders(ModelBinderDictionary modelBinders)
        {
            modelBinders[typeof(SearchCriteria)] = new Oxite.Modules.Search.ModelBinders.SearchCriteriaModelBinder();
            modelBinders[typeof(PagingInfo)] = new PagingInfoModelBinder();
        }

        public void RegisterWithContainer()
        {
            container
                .RegisterType<ISearchResultService, BingSearchResultService>();
        }

        #endregion
    }
}
