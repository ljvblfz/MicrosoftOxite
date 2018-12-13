//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using Microsoft.Practices.Unity;
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
                .RegisterType<IBackgroundServiceRepository, BackgroundServiceRepository>()
                .RegisterType<IUserRepository, UserRepository>()
                .RegisterType<ITagRepository, TagRepository>()
                .RegisterType<IMessageOutboundRepository, MessageOutboundRepository>()
                .RegisterType<ITrackbackOutboundRepository, TrackbackOutboundRepository>()
                .RegisterType<IPageRepository, PageRepository>()
                .RegisterType<IAreaRepository, AreaRepository>()
                .RegisterType<ILocalizationRepository, LocalizationRepository>()
                .RegisterType<ILanguageRepository, LanguageRepository>()
                .RegisterType<IPostRepository, PostRepository>();

            return container;
        }
    }
}
