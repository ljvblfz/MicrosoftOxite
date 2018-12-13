//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Oxite.Configuration;
using Oxite.Infrastructure;
using Oxite.ModelBinders;
using Oxite.Modules.Search.ModelBinders;
using Oxite.Modules.Search.Models;
using Oxite.Modules.Search.Repositories;
using Oxite.Modules.Search.Repositories.SqlServer;
using Oxite.Modules.Search.Services;
using Oxite.Routing;

namespace Oxite.Modules.Search
{
    public class SearchModule : IOxiteModule, IOxiteDataProvider
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
            modelBinders[typeof(SearchCriteria)] = new SearchCriteriaModelBinder();
            modelBinders[typeof(PagingInfo)] = new PagingInfoModelBinder();
        }

        public void RegisterWithContainer()
        {
            container
                .RegisterType<ISearchResultService, SearchResultService>();
        }

        #endregion

        #region IOxiteDataProvider Members

        public void ConfigureProvider(OxiteConfigurationSection config, OxiteDataProviderConfigurationElement dataProviderConfig, IUnityContainer container)
        {
            if (dataProviderConfig.Category == "LinqToSql")
                container
                    .RegisterType<OxiteSearchDataContext>(new InjectionConstructor(new ResolvedParameter<string>(!string.IsNullOrEmpty(dataProviderConfig.ConnectionString) ? dataProviderConfig.ConnectionString : config.Providers.DefaultConnectionString)))
                    .RegisterType<ISearchResultRepository, SqlServerSearchResultRepository>();
        }

        #endregion
    }
}
