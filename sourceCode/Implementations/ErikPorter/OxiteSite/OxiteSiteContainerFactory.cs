using Microsoft.Practices.Unity;
//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using Oxite;
using Oxite.LinqToSqlDataProvider;
using Oxite.Repositories;

namespace OxiteSite
{
    public class OxiteSiteContainerFactory : ContainerFactory
    {
        public override IUnityContainer GetOxiteContainer()
        {
            IUnityContainer container  = base.GetOxiteContainer();

            container
                .RegisterType<OxiteLinqToSqlDataContext>(
                    new InjectionConstructor(new ResolvedParameter<string>("ApplicationServices")))
                .RegisterType<ISiteRepository, SiteRepository>()
                .RegisterType<IPluginRepository, PluginRepository>()
                .RegisterType<IUserRepository, UserRepository>()
                .RegisterType<ITagRepository, TagRepository>()
                .RegisterType<IPageRepository, PageRepository>()
                .RegisterType<IAreaRepository, AreaRepository>()
                .RegisterType<ILocalizationRepository, LocalizationRepository>()
                .RegisterType<ILanguageRepository, LanguageRepository>()
                .RegisterType<IPostRepository, PostRepository>()
                .RegisterType<IViewTrackingRepository, ViewTrackingRepository>();

            return container;
        }
    }
}
