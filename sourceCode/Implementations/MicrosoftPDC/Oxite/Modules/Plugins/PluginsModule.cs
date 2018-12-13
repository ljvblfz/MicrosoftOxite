//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Oxite.Filters;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Plugins.Controllers;
using Oxite.Modules.Plugins.Extensions;
using Oxite.Modules.Plugins.Filters;
using Oxite.Modules.Plugins.Models;
using Oxite.Modules.Plugins.ModelBinders;
using Oxite.Modules.Plugins.Repositories;
using Oxite.Modules.Plugins.Repositories.SqlServer;
using Oxite.Modules.Plugins.Services;
using Oxite.Modules.Plugins.Validation;
using Oxite.Plugins;
using Oxite.Plugins.Extensions;
using Oxite.Plugins.Models;
using Oxite.Routing;
using Oxite.Services;
using Oxite.Validation;

namespace Oxite.Modules.Plugins
{
    public class PluginsModule : IOxiteModule
    {
        private readonly IUnityContainer container;

        public PluginsModule(IUnityContainer container)
        {
            this.container = container;
        }

        #region IOxiteModule Members

        public void Initialize()
        {
            ISiteService siteService = container.Resolve<ISiteService>();
            IPluginEngine pluginEngine = container.Resolve<IPluginEngine>();
            PluginTemplateRegistry pluginTemplateRegistry = container.Resolve<PluginTemplateRegistry>();
            RouteCollection routes = container.Resolve<RouteCollection>();
            PluginScriptRegistry pluginScriptRegistry = container.Resolve<PluginScriptRegistry>();
            PluginStyleRegistry pluginStyleRegistry = container.Resolve<PluginStyleRegistry>();

            pluginEngine.AutoInitializePlugins = false;

            // load dynamically compiled assemblies
            pluginEngine.LoadAssembliesFromCodeFiles("~" + siteService.GetSite().PluginsPath);

            // load plugins that are in assemblies
            pluginEngine.LoadPlugins();

            // initialize plugins that are installed
            foreach (Plugin plugin in container.Resolve<IPluginService>().GetPlugins())
            {
                PluginContainer pluginContainer = pluginEngine.GetPlugin(plugin);

                if (pluginContainer != null)
                {
                    plugin.Container = pluginContainer;
                    pluginContainer.Tag = plugin;

                    if (pluginContainer.IsLoaded)
                    {
                        pluginContainer.ApplyProperties(plugin.ExtendedProperties);
                        pluginContainer.Initialize();

                        if (plugin.Enabled)
                        {
                            pluginContainer.RegisterTemplates(pluginTemplateRegistry);
                            pluginContainer.RegisterRoutes(routes);
                            pluginContainer.RegisterScripts(pluginScriptRegistry);
                            pluginContainer.RegisterStyles(pluginStyleRegistry);
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(plugin.VirtualPath))
                        pluginContainer = new PluginContainer(new PluginAssemblyContainer(plugin.VirtualPath, (Assembly)null), new PluginFileNotFoundException(plugin.VirtualPath));

                    if (pluginContainer != null)
                    {
                        plugin.Container = pluginContainer;
                        pluginContainer.Tag = plugin;

                        pluginEngine.AddPlugin(pluginContainer);
                    }
                }
            }

            container.RegisterInstance(pluginStyleRegistry);
            container.RegisterInstance(pluginScriptRegistry);
            container.RegisterInstance(pluginTemplateRegistry);
            container.RegisterInstance(pluginEngine);
        }

        public void Unload()
        {
            IPluginEngine pluginEngine = container.Resolve<IPluginEngine>();

            // unload plugins that are installed
            foreach (Plugin plugin in container.Resolve<IPluginService>().GetPlugins())
            {
                PluginContainer pluginContainer = pluginEngine.GetPlugin(plugin);

                if (pluginContainer != null)
                    pluginContainer.Unload();
            }

            container.RegisterInstance(pluginEngine);
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            string[] controllerNamespaces = new string[] { "Oxite.Modules.Plugins.Controllers" };

            routes.MapRoute(
                "Plugins",
                "Admin/Plugins",
                new { controller = "Plugin", action = "List", role = "Admin" },
                null,
                controllerNamespaces
                );

            routes.MapRoute(
                "PluginsInstalled",
                "Admin/Plugins/Installed",
                new { controller = "Plugin", action = "List", installed = true, role = "Admin" },
                null,
                controllerNamespaces
                );

            routes.MapRoute(
                "PluginsNotInstalled",
                "Admin/Plugins/NotInstalled",
                new { controller = "Plugin", action = "List", installed = false, role = "Admin" },
                null,
                controllerNamespaces
                );

            routes.MapRoute(
                "PluginInstall",
                "Admin/Plugins/Install",
                new { controller = "Plugin", action = "Install", role = "Admin", validateAntiForgeryToken = true },
                new { httpMethod = new HttpMethodConstraint("POST") },
                controllerNamespaces
                );

            routes.MapRoute(
                "PluginsNotInstalledRefresh",
                "Admin/Plugins/Install/Reload",
                new { controller = "Plugin", action = "RefreshListNotInstalled", role = "Admin", validateAntiForgeryToken = true },
                new { httpMethod = new HttpMethodConstraint("POST") },
                controllerNamespaces
                );

            routes.MapRoute(
                "PluginEdit",
                "Admin/Plugins/{pluginID}/Edit",
                new { controller = "Plugin", action = "ItemEdit", role = "Admin" },
                new { pluginID = new IsGuid(), httpMethod = new HttpMethodConstraint("GET") },
                controllerNamespaces
                );

            routes.MapRoute(
                "PluginEditNotInstalled",
                "Admin/Plugins/NotInstalled/Edit",
                new { controller = "Plugin", action = "EditNotInstalled", role = "Admin", validateAntiForgeryToken = true },
                new { httpMethod = new HttpMethodConstraint("POST") },
                controllerNamespaces
                );

            routes.MapRoute(
                "PluginReloadNotInstalled",
                "Admin/Plugins/NotInstalled/Reload",
                new { controller = "Plugin", action = "ReloadNotInstalled", role = "Admin", validateAntiForgeryToken = true },
                new { httpMethod = new HttpMethodConstraint("POST") },
                controllerNamespaces
                );

            routes.MapRoute(
                "PluginEditCode",
                "Admin/Plugins/{pluginID}/Edit",
                new { controller = "Plugin", action = "ItemEdit", isEditingCode = true, role = "Admin", validateAntiForgeryToken = true },
                new { pluginID = new IsGuid(), code = new IsInFormCollection(), httpMethod = new HttpMethodConstraint("POST") },
                controllerNamespaces
                );

            routes.MapRoute(
                "PluginEditSettings",
                "Admin/Plugins/{pluginID}/Edit",
                new { controller = "Plugin", action = "ItemEdit", isEditingCode = false, role = "Admin", validateAntiForgeryToken = true },
                new { pluginID = new IsGuid(), httpMethod = new HttpMethodConstraint("POST") },
                controllerNamespaces
                );

            routes.MapRoute(
                "PluginEnable",
                "Admin/Plugins/{pluginID}/Enable",
                new { controller = "Plugin", action = "SetEnabled", enabled = true, role = "Admin", validateAntiForgeryToken = true },
                new { pluginID = new IsGuid(), httpMethod = new HttpMethodConstraint("POST") },
                controllerNamespaces
                );

            routes.MapRoute(
                "PluginDisable",
                "Admin/Plugins/{pluginID}/Disable",
                new { controller = "Plugin", action = "SetEnabled", enabled = false, role = "Admin", validateAntiForgeryToken = true },
                new { pluginID = new IsGuid(), httpMethod = new HttpMethodConstraint("POST") },
                controllerNamespaces
                );

            routes.MapRoute(
                "PluginUninstall",
                "Admin/Plugins/{pluginID}/Uninstall",
                new { controller = "Plugin", action = "Uninstall", role = "Admin", validateAntiForgeryToken = true },
                new { pluginID = new IsGuid(), httpMethod = new HttpMethodConstraint("POST") },
                controllerNamespaces
                );

            routes.MapRoute(
                "PluginReload",
                "Admin/Plugins/{pluginID}/Reload",
                new { controller = "Plugin", action = "Reload", role = "Admin", validateAntiForgeryToken = true },
                new { pluginID = new IsGuid(), httpMethod = new HttpMethodConstraint("POST") },
                controllerNamespaces
                );
        }

        public void RegisterCatchAllRoutes(RouteCollection routes)
        {
        }

        public void RegisterFilters(IFilterRegistry filterRegistry)
        {
            filterRegistry.Add(Enumerable.Empty<IFilterCriteria>(), typeof(PluginTemplateFilter));

            ControllerActionFilterCriteria ajaxActionCriteria = new ControllerActionFilterCriteria();
            ajaxActionCriteria.AddMethod<PluginController>(p => p.List(null));
            ajaxActionCriteria.AddMethod<PluginController>(p => p.RefreshListNotInstalled());
            ajaxActionCriteria.AddMethod<PluginController>(p => p.SetEnabled(null, false, null, null));
            ajaxActionCriteria.AddMethod<PluginController>(p => p.Install(null, null));
            ajaxActionCriteria.AddMethod<PluginController>(p => p.Uninstall(null, null));
            filterRegistry.Add(new[] { ajaxActionCriteria }, typeof(AjaxActionFilter));
        }

        public void RegisterModelBinders(ModelBinderDictionary modelBinders)
        {
            modelBinders[typeof(PluginAddress)] = new PluginAddressModelBinder();
            modelBinders[typeof(PluginEditInput)] = new PluginEditInputModelBinder();
            modelBinders[typeof(PluginNotInstalledAddress)] = new PluginNotInstalledAddressModelBinder();
            modelBinders[typeof(PluginInstallInput)] = new PluginInstallInputModelBinder();
        }

        public void RegisterWithContainer()
        {
            container
                .RegisterType<IPluginService, PluginService>()
                .RegisterType<IValidator<PluginPropertiesInput>, PluginPropertiesInputValidator>();

            //TODO: (erikpo) Add a check here for if the app is running on sql or xml files
            container
                .RegisterType<OxitePluginsDataContext>(new InjectionConstructor(new ResolvedParameter<string>("ApplicationServices")))
                .RegisterType<IPluginRepository, SqlServerPluginRepository>();
        }

        #endregion
    }
}
