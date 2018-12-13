//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Oxite.Configuration;
using Oxite.Configuration.Extensions;
using Oxite.Infrastructure;
using Oxite.Modules.Messages.BackgroundServices;
using Oxite.Modules.Messages.Repositories;
using Oxite.Modules.Messages.Repositories.SqlServer;
using Oxite.Modules.Messages.Services;

namespace Oxite.Modules.Messages
{
    public class MessagesModule : IOxiteModule, IOxiteDataProvider, IOxiteBackgroundService
    {
        private readonly IUnityContainer container;

        public MessagesModule(IUnityContainer container)
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
        }

        public void RegisterWithContainer()
        {
            container
                .RegisterType<IMessageOutboundService, MessageOutboundService>();
        }

        #endregion

        #region IOxiteDataProvider Members

        public void ConfigureProvider(OxiteConfigurationSection config, OxiteDataProviderConfigurationElement dataProviderConfig, IUnityContainer container)
        {
            if (dataProviderConfig.Category == "LinqToSql")
                container
                    .RegisterType<OxiteMessagesDataContext>(new InjectionConstructor(new ResolvedParameter<string>(!string.IsNullOrEmpty(dataProviderConfig.ConnectionString) ? dataProviderConfig.ConnectionString : config.Providers.DefaultConnectionString)))
                    .RegisterType<IMessageOutboundRepository, SqlServerMessageOutboundRepository>();
        }

        #endregion

        #region IOxiteBackgroundService Members

        public void RegisterBackgroundServices(IBackgroundServiceRegistry registry)
        {
            //OxiteConfigurationSection config = container.Resolve<OxiteConfigurationSection>();
            //OxiteModuleConfigurationElement moduleConfiguration = config.Modules.Module(this.GetType());

            //registry.Add<InProcessBackgroundServiceExecutor, SendMessages>(moduleConfiguration, "SendTrackbacks", TimeSpan.FromMinutes(1));
        }

        #endregion
    }
}