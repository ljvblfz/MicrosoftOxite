//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------

namespace Oxite
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Web;
    using System.Web.Hosting;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.Configuration;
    using Oxite.BootStrapperTasks;
    using Oxite.Configuration;
    using Oxite.Extensions;
    using Oxite.Infrastructure;
    using Oxite.Models;
    using Oxite.Plugins;
    using Oxite.Repositories;
    using Oxite.Repositories.SqlServer;
    using Oxite.Services;
    using Oxite.Validation;

    /// <summary>
    /// Class that acts as main entry point for Oxite.
    /// </summary>
    public class OxiteApplication : HttpApplication
    {
        public OxiteApplication()
        {
            this.AcquireRequestState += OxiteApplication_AcquireRequestState;
        }

        private void OxiteApplication_AcquireRequestState(object sender, EventArgs e)
        {
            MvcHandler handler = Context.Handler as MvcHandler;

            if (handler != null)
            {
                RequestContext requestContext = handler.RequestContext;

                if (requestContext != null)
                {
                    IUnityContainer container = ((IUnityContainer)Application["container"]);

                    if (container != null)
                    {
                        IModulesLoaded modules = container.Resolve<IModulesLoaded>();

                        if (modules != null)
                        {
                            IUser user = new UserAnonymous();

                            foreach (IOxiteAuthenticationModule module in modules.GetModules<IOxiteAuthenticationModule>().Reverse())
                            {
                                user = module.GetUser(requestContext);

                                if (user.IsAuthenticated)
                                    break;
                            }

                            Context.Items[typeof(Site).FullName] = container.Resolve<ISiteService>().GetSite();
                            Context.Items[typeof(IUser).FullName] = user;
                            Context.Items[typeof(RequestContext).FullName] = requestContext;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Setup and load application
        /// </summary>
        protected void Application_Start()
        {
            Application["container"] = setupContainer();
            Application["bootStrappersLoaded"] = false;

            load();
        }

        /// <summary>
        /// Clean up and necessary objects in the application
        /// </summary>
        protected void Application_End()
        {
            unload();
        }

        /// <summary>
        /// Handle any requests made to the application.
        /// </summary>
        /// <param name="sender">Sender of request.</param>
        /// <param name="e">EventArgs for request.</param>
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            Site site = ((IUnityContainer)Application["container"]).Resolve<ISiteService>().GetSite();

            // If no site entry exists, redirect to the setup url
            if (site == null)
            {
                string setupUrl = new UrlHelper(new RequestContext(new HttpContextWrapper(Context), new RouteData())).Site();

                bool isSetupUrl = Request.RawUrl.EndsWith(setupUrl, StringComparison.OrdinalIgnoreCase);
                bool isNotSkinned = Request.RawUrl.IndexOf("/Skins", StringComparison.OrdinalIgnoreCase) == -1;
                bool isContent = Request.RawUrl.StartsWith("/Content", StringComparison.OrdinalIgnoreCase);

                if (!isSetupUrl && isNotSkinned && !isContent)
                {
                    Response.Redirect(setupUrl, true);
                }
            }

            if (site.ID != Guid.Empty && !this.hasSameHostAsRequest(site.Host))
            {
                Uri hostAlias = site.HostRedirects.Where(this.hasSameHostAsRequest).FirstOrDefault();

                if (hostAlias != null)
                {
                    Response.RedirectLocation = this.makeHostMatchRequest(site.Host).ToString();
                    Response.StatusCode = 301;
                    Response.End();
                }
            }

            // TODO: (erikpo) Need to find a better way for legacy apps to get access to the original HttpRequest and HttpResponse instead of HttpRequestBase and HttpResponseBase
            Context.Items["originalRequest"] = Context.Request;
            Context.Items["originalResponse"] = Context.Response;
        }

        /// <summary>
        /// Executes all BootStrapperTasks in the given HttpContextBase
        /// </summary>
        /// <param name="context">HttpContextBase containing BootStrapperTasks to execute.</param>
        public static void Load(HttpContextBase context)
        {
            IEnumerable<IBootStrapperTask> tasks = ((IUnityContainer)context.Application["container"]).ResolveAll<IBootStrapperTask>();
            bool bootStrappersLoaded = (bool)context.Application["bootStrappersLoaded"];
            IDictionary<string, object> state = (IDictionary<string, object>)context.Application["bootStrapperState"];

            if (state == null)
            {
                context.Application["bootStrapperState"] = state = new Dictionary<string, object>();
            }

            // If the tasks have been executed previously, call Cleanup on them to rollback any changes
            // they caused.
            if (bootStrappersLoaded)
            {
                foreach (IBootStrapperTask task in tasks)
                {
                    task.Cleanup(state);
                }
            }

            foreach (IBootStrapperTask task in tasks)
            {
                task.Execute(state);
            }

            context.Application["bootStrappersLoaded"] = true;
        }

        /// <summary>
        /// Initialize any needed info for Oxite.
        /// </summary>
        private void load()
        {
            Load(new HttpContextWrapper(Context));
        }

        /// <summary>
        /// Make sure all changes made by Oxite are cleaned up.  Execute Cleanup on core BootStrapperTasks.
        /// </summary>
        private void unload()
        {
            if (Context != null && Context.Application != null)
            {
                IDictionary<string, object> state = (IDictionary<string, object>)Context.Application["bootStrapperState"];

                foreach (IBootStrapperTask task in ((IUnityContainer)Context.Application["container"]).ResolveAll<IBootStrapperTask>())
                    task.Cleanup(state);
            }
        }

        /// <summary>
        /// Verifies that the current request is actually for the Oxite site.
        /// </summary>
        /// <param name="url">Url of the host of the Oxite site.</param>
        /// <returns>True if the request is for Oxite.  False otherwise.</returns>
        private bool hasSameHostAsRequest(Uri url)
        {
            // Make sure that the request scheme and the site scheme match.  ie: both are http
            if (!string.Equals(url.Scheme, Request.Url.Scheme, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            // Verify that the request is actually for the Oxite site.  ie: both hosts are www.mysite.com
            if (!string.Equals(url.Host, Request.Url.Host, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return url.Port == Request.Url.Port;
        }

        /// <summary>
        /// Build a Uri using the current Request.Uri and changing the scheme, host, and port to that
        /// of the Oxite site.
        /// </summary>
        /// <param name="url">Url of the host of the Oxite site.</param>
        /// <returns>Uri using the current Request.Uri and changing the scheme, host, and port to that
        /// of the Oxite site.</returns>
        private Uri makeHostMatchRequest(Uri url)
        {
            if (url == null)
            {
                return null;
            }

            UriBuilder builder = new UriBuilder(Request.Url);
            UriBuilder builder2 = new UriBuilder(url);

            builder.Scheme = builder2.Scheme;
            builder.Host = builder2.Host;
            builder.Port = builder2.Port;

            return builder.Uri;
        }

        /// <summary>
        /// Create Unity containers to be used by Oxite and fill them with relevant data.
        /// </summary>
        /// <returns>IUnityContainer to use when resolving objects throughout Oxite.</returns>
        private IUnityContainer setupContainer()
        {
            IUnityContainer parentContainer = new UnityContainer();

            parentContainer
                .RegisterInstance((OxiteConfigurationSection)ConfigurationManager.GetSection("oxite"))
                .RegisterInstance(new AppSettingsHelper(ConfigurationManager.AppSettings))
                .RegisterInstance(RouteTable.Routes)
                .RegisterInstance(System.Web.Mvc.ModelBinders.Binders)
                .RegisterInstance(ViewEngines.Engines)
                .RegisterInstance(HostingEnvironment.VirtualPathProvider);

            foreach (ConnectionStringSettings connectionString in ConfigurationManager.ConnectionStrings)
                parentContainer.RegisterInstance(connectionString.Name, connectionString.ConnectionString);

            foreach (ConnectionStringSettings connectionString in parentContainer.Resolve<OxiteConfigurationSection>().ConnectionStrings)
                parentContainer.RegisterInstance(connectionString.Name, connectionString.ConnectionString);

            parentContainer
                .RegisterInstance<IBootStrapperTask>("LoadModules", new LoadModules(parentContainer))
                .RegisterInstance<IBootStrapperTask>("LoadBackgroundServices", new LoadBackgroundServices(parentContainer));

            parentContainer
                .RegisterType<IModulesLoaded, ModulesLoaded>()
                .RegisterType<IPluginEngine, PluginEngine>()
                .RegisterType<ISiteService, SiteService>()
                .RegisterType<IValidationService, ValidationService>()
                .RegisterType<Site>(new FactoryMethodLifetimeManager(() => HttpContext.Current.Items[typeof(Site).FullName] as Site ?? parentContainer.Resolve<ISiteService>().GetSite()))
                .RegisterType<IUser>(new FactoryMethodLifetimeManager(() => new UserLazy(() => HttpContext.Current.Items[typeof(IUser).FullName] as IUser ?? new UserEmpty())))
                .RegisterType<RequestContext>(new FactoryMethodLifetimeManager(() => HttpContext.Current.Items[typeof(RequestContext).FullName] as RequestContext ?? new RequestContext(new HttpContextWrapper(HttpContext.Current), new RouteData())));

            // TODO: (erikpo) Add a check here for if the app is running on sql or xml files
            parentContainer
                .RegisterType<OxiteDataContext>(new InjectionConstructor(new ResolvedParameter<string>("ApplicationServices")))
                .RegisterType<ISiteRepository, SqlServerSiteRepository>()
                .RegisterType<ILocalizationRepository, SqlServerLocalizationRepository>()
                .RegisterType<ILanguageRepository, SqlServerLanguageRepository>()
                .RegisterType<IExtendedPropertyRepository, SqlServerExtendedPropertyRepository>();

            // Create a child container and configure it using any settings in the Unity configuration section of the 
            // web.config file.  This will allow users to override any of the default Oxite object mappings within
            // the config file as Unity will search the child container for dependencies first.
            IUnityContainer oxiteContainer = parentContainer.CreateChildContainer();

            UnityConfigurationSection config = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");

            if (config.Containers.Default != null)
            {
                config.Containers.Default.Configure(oxiteContainer);
            }

            oxiteContainer.RegisterInstance(oxiteContainer);

            return oxiteContainer;
        }
    }
}
