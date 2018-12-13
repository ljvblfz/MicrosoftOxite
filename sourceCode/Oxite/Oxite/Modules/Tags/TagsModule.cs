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
using Oxite.Modules.Tags.ModelBinders;
using Oxite.Modules.Tags.Models;
using Oxite.Modules.Tags.Repositories;
using Oxite.Modules.Tags.Repositories.SqlServer;
using Oxite.Modules.Tags.Services;

namespace Oxite.Modules.Tags
{
    public class TagsModule : IOxiteModule, IOxiteDataProvider
    {
        private readonly IUnityContainer container;

        public TagsModule(IUnityContainer container)
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
        }

        public void RegisterCatchAllRoutes(RouteCollection routes)
        {
        }

        public void RegisterFilters(IFilterRegistry filterRegistry)
        {
        }

        public void RegisterModelBinders(ModelBinderDictionary modelBinders)
        {
            modelBinders[typeof(Tag)] = container.Resolve<TagModelBinder>();
        }

        public void RegisterWithContainer()
        {
            container
                .RegisterType<ITagService, TagService>();
        }

        #endregion

        #region IOxiteDataProvider Members

        public void ConfigureProvider(OxiteConfigurationSection config, OxiteDataProviderConfigurationElement dataProviderConfig, IUnityContainer container)
        {
            if (dataProviderConfig.Category == "LinqToSql")
                container
                    .RegisterType<OxiteTagsDataContext>(new InjectionConstructor(new ResolvedParameter<string>(!string.IsNullOrEmpty(dataProviderConfig.ConnectionString) ? dataProviderConfig.ConnectionString : config.Providers.DefaultConnectionString)))
                    .RegisterType<ITagRepository, SqlServerTagRepository>();
        }

        #endregion
    }
}
