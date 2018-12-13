//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Oxite.Filters;
using Oxite.Infrastructure;
using Oxite.Modules.Search.Controllers;
using Oxite.Modules.Search.ModelBinders;
using Oxite.Modules.Search.Models;
using Oxite.Modules.Search.Repositories;
using Oxite.Modules.Search.Repositories.SqlServer;
using Oxite.Modules.Search.Services;
using Oxite.Routing;

namespace Oxite.Modules.Search
{
    public class SearchModule : IOxiteModule
    {
        private readonly IUnityContainer container;

        public SearchModule(IUnityContainer container)
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
            listActionsCriteria.AddMethod<SearchController>(p => p.List(null, 0, null));
            filterRegistry.Add(new[] { listActionsCriteria }, typeof(PageSizeActionFilter));
        }

        public void RegisterModelBinders(ModelBinderDictionary modelBinders)
        {
            modelBinders[typeof(SearchCriteria)] = new SearchCriteriaModelBinder();
        }

        public void RegisterWithContainer()
        {
            container
                .RegisterType<ISearchResultService, SearchResultService>();

            //TODO: (erikpo) Once there is a xml file provider, put this in an if statement based off of the site setting for which provider to use
            container
                .RegisterType<OxiteSearchDataContext>(new InjectionConstructor(new ResolvedParameter<string>("ApplicationServices")))
                .RegisterType<ISearchResultRepository, SqlServerSearchResultRepository>();
        }

        #endregion
    }
}
