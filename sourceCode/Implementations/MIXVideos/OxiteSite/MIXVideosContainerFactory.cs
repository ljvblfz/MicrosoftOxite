//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web;
using Microsoft.Practices.Unity;
using MIXVideos.Oxite.BackgroundServices;
using MIXVideos.Oxite.CachingRepositories;
using MIXVideos.Oxite.Infrastructure;
using MIXVideos.Oxite.Models;
using MIXVideos.Oxite.Repositories;
using MIXVideos.Oxite.Routing;
using MIXVideos.Oxite.Services;
using Oxite;
using Oxite.BackgroundServices;
using Oxite.Infrastructure;
using Oxite.LinqToSqlDataProvider;
using Oxite.Repositories;
using Oxite.Skinning;
using Oxite.Services;

namespace OxiteSite
{
    public class MIXVideosContainerFactory : ContainerFactory
    {
        public override IUnityContainer GetOxiteContainer()
        {
            IUnityContainer baseContainer = base.GetOxiteContainer();

            baseContainer
                .RegisterInstance(new PostViewStore());

            baseContainer
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
                .RegisterType<IPostRepository, PostRepository>("inner")
                .RegisterType<IPostRepository, CachingPostRepository>(
                    new InjectionConstructor(new ResolvedParameter<IPostRepository>("inner"), new InjectionParameter<ICache>(new ApplicationCache(HttpRuntime.Cache))))
                .RegisterType<IRegisterFilters, MIXVideosRegisterFilters>("MIXVideosRoutes")
                .RegisterType<OxitePostViewDataContext>(
                    new InjectionConstructor(new ResolvedParameter<string>("ApplicationServices")))
                .RegisterType<IPostViewService, PostViewService>()
                .RegisterType<IPostViewRepository, PostViewRepository>()
                .RegisterType<IRegisterRoutes, MIXVideosRegisterRoutes>("MIXVideosRegisterRoutes")
                .RegisterType<IBackgroundService, SaveViewsBackgroundService>("MIXVideosViewTracking")
                .RegisterType<ISkinResolver, LegacySkinResolver>("LegacySkinResolver")
                .RegisterType<ISpamFilterService, AkismetSpamFilterService>();

            return baseContainer;
        }
    }
}
