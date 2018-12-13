//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Oxite.Infrastructure;
using Oxite.Modules.Tags.ModelBinders;
using Oxite.Modules.Tags.Models;
using Oxite.Modules.Tags.Repositories;
using Oxite.Modules.Tags.Repositories.SqlServer;
using Oxite.Modules.Tags.Services;

namespace Oxite.Modules.Tags
{
    public class TagsModule : IOxiteModule
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
            modelBinders[typeof(TagAddress)] = new TagAddressModelBinder();
        }

        public void RegisterWithContainer()
        {
            container
                .RegisterType<ITagService, TagService>();

            //TODO: (erikpo) Once there is a xml file provider, put this in an if statement based off of the site setting for which provider to use
            container
                .RegisterType<OxiteTagsDataContext>(new InjectionConstructor(new ResolvedParameter<string>("ApplicationServices")))
                .RegisterType<ITagRepository, SqlServerTagRepository>();
        }

        #endregion
    }
}
