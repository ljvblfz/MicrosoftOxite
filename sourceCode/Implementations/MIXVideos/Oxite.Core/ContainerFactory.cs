//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Configuration;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Oxite.BootStrappers;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Services;
using Oxite.Skinning;
using Oxite.Validation;
using Oxite.BackgroundServices;

namespace Oxite
{
    public class ContainerFactory
    {
        public virtual IUnityContainer GetOxiteContainer()
        {
            IUnityContainer parentContainer = new UnityContainer();

            parentContainer
                .RegisterInstance(new AppSettingsHelper(ConfigurationManager.AppSettings))
                .RegisterInstance(RouteTable.Routes)
                .RegisterInstance(ModelBinders.Binders)
                .RegisterInstance(ViewEngines.Engines)
                .RegisterInstance(HostingEnvironment.VirtualPathProvider);

            foreach (ConnectionStringSettings connectionString in ConfigurationManager.ConnectionStrings)
                parentContainer.RegisterInstance(connectionString.Name, connectionString.ConnectionString);

            parentContainer
                .RegisterType<ISiteService, SiteService>()
                .RegisterType<IBackgroundServiceService, BackgroundServiceService>()
                .RegisterType<IUserService, UserService>()
                .RegisterType<ITagService, TagService>()
                .RegisterType<IPostService, PostService>()
                .RegisterType<IPageService, PageService>()
                .RegisterType<IAreaService, AreaService>()
                .RegisterType<ISpamFilterService, NaiveSpamFilterService>()
                .RegisterType<ILocalizationService, LocalizationService>()
                .RegisterType<ILanguageService, LanguageService>()
                .RegisterType<IMessageOutboundService, MessageOutboundService>()
                .RegisterType<ITrackbackOutboundService, TrackbackOutboundService>()
                .RegisterType<IActionInvoker, OxiteControllerActionInvoker>()
                .RegisterType<IFormsAuthentication, FormsAuthenticationWrapper>()
                .RegisterType<IViewEngine, OxiteWebFormViewEngine>()
                .RegisterType<IValidationService, ValidationService>()
                .RegisterType<IRegularExpressions, RegularExpressions>()
                .RegisterType<IValidator<Comment>, CommentValidator>()
                .RegisterType<IValidator<UserBase>, UserBaseValidator>()
                .RegisterType<IValidator<PostSubscription>, PostSubscriptionValidator>()
                .RegisterType<IValidator<User>, UserValidator>()
                .RegisterType<IValidator<Site>, SiteValidator>()
                .RegisterType<IValidator<Area>, AreaValidator>()
                .RegisterType<IValidator<Post>, PostValidator>()
                .RegisterType<IValidator<Page>, PageValidator>()
                .RegisterType<IRegisterFilters, OxiteRegisterFilters>()
                .RegisterType<IRegisterRoutes, OxiteRegisterRoutes>()
                .RegisterType<IRegisterModelBinders, OxiteRegisterModelBinders>()
                .RegisterType<IRouteModifier, OxiteRouteUrlModifier>()
                .RegisterType<IBackgroundService, SendTrackbacks>("Trackbacks")
                .RegisterType<IBackgroundService, SendMessages>("Messages")
                .RegisterType<ISkinResolverRegistry>()
                .RegisterType<ISkinResolver, OxiteSkinResolver>()
                .RegisterType<ISkinResolver, MobileSkinResolver>("MobileSkinResolver");

            IUnityContainer oxiteContainer = parentContainer.CreateChildContainer();

            UnityConfigurationSection config = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            if (config.Containers.Default != null)
                config.Containers.Default.Configure(oxiteContainer);

            oxiteContainer.RegisterInstance(oxiteContainer);

            oxiteContainer
                .RegisterInstance<IBootStrapperTask>("RegisterSite", new RegisterSite(oxiteContainer))
                .RegisterInstance<IBootStrapperTask>("RegisterRoutes", new RegisterRoutes(oxiteContainer))
                .RegisterInstance<IBootStrapperTask>("RegisterFilters", new RegisterFilters(oxiteContainer))
                .RegisterInstance<IBootStrapperTask>("RegisterModelBinders", new RegisterModelBinders(oxiteContainer))
                .RegisterInstance<IBootStrapperTask>("RegisterControllerFactory", new RegisterControllerFactory(oxiteContainer))
                .RegisterInstance<IBootStrapperTask>("RegisterViewEngines", new RegisterViewEngines(oxiteContainer))
                .RegisterInstance<IBootStrapperTask>("RegisterSkinResolvers", new RegisterSkinResolvers(oxiteContainer))
                .RegisterInstance<IBootStrapperTask>("RegisterBackgroundServices", new RegisterBackgroundServices(oxiteContainer));

            return oxiteContainer;
        }
    }
}
