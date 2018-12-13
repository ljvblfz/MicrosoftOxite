//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Repositories;

namespace Oxite.LinqToSqlDataProvider
{
    public class PluginRepository : IPluginRepository
    {
        private OxiteLinqToSqlDataContext context;
        private Guid siteID;

        public PluginRepository(OxiteLinqToSqlDataContext context, Site site)
        {
            this.context = context;
            this.siteID = site.ID;
        }

        #region IPluginRepository Members

        public IList<IPlugin> GetPlugins()
        {
            var query =
                from p in context.oxite_Plugins
                where p.SiteID == siteID
                orderby p.PluginCategory/*, p.PluginDisplayName*/, p.PluginName
                select p;

            return query.Select(
                p =>
                    new Plugin(p.PluginID, p.PluginCategory, p.PluginName, new NameValueCollection())
                    {
                        //DisplayName = p.PluginDisplayName,
                        Enabled = p.Enabled
                    }
                ).Cast<IPlugin>().ToList();
        }

        //TODO: (erikpo) Not sure if this code should be broken up or where if it should live somewhere else.
        public IList<IPlugin> GetPluginsNotInstalled()
        {
            IList<IPlugin> pluginsInstalled = GetPlugins();
            //TODO: (erikpo) Change Plugins path to not be hardcoded
            string pluginsFilePath = System.Web.HttpContext.Current.Server.MapPath("/Plugins");
            List<IPlugin> pluginsNotInstalled = new List<IPlugin>(10);

            foreach (string categoryDir in System.IO.Directory.GetDirectories(pluginsFilePath))
            {
                string categoryName = categoryDir.Remove(0, System.IO.Path.GetDirectoryName(categoryDir).Length + 1);

                foreach (string pluginDir in System.IO.Directory.GetDirectories(categoryDir))
                {
                    string pluginName = pluginDir.Remove(0, System.IO.Path.GetDirectoryName(pluginDir).Length + 1);
                    string pluginBinDir = System.IO.Path.Combine(pluginDir, "bin");

                    if (System.IO.Directory.Exists(pluginBinDir))
                    {
                        string expectedAssemblyPath = System.IO.Path.Combine(pluginBinDir, pluginName + ".dll");

                        if (System.IO.File.Exists(expectedAssemblyPath) && !pluginsInstalled.Where(p => string.Compare(p.Category, categoryName, true) == 0 && string.Compare(p.Name, pluginName, true) == 0).Any())
                        {
                            pluginsNotInstalled.Add(new Plugin(Guid.Empty, categoryName, pluginName, new NameValueCollection()));
                        }
                    }
                }
            }

            return pluginsNotInstalled;
        }

        public IPlugin GetPlugin(Guid id)
        {
            return (
                from p in context.oxite_Plugins
                let pluginSettings = getPluginSettings(p)
                where p.SiteID == siteID && p.PluginID == id
                select new Plugin(p.PluginID, p.PluginCategory, p.PluginName, projectPluginSettings(pluginSettings))
                {
                    Enabled = p.Enabled,
                }
                ).FirstOrDefault();
        }

        private IQueryable<oxite_PluginSetting> getPluginSettings(oxite_Plugin plugin)
        {
            return
                from ps in context.oxite_PluginSettings
                where ps.SiteID == siteID && ps.PluginID == plugin.PluginID
                select ps;
        }

        private NameValueCollection projectPluginSettings(IQueryable<oxite_PluginSetting> pluginSettings)
        {
            NameValueCollection settings = new NameValueCollection();

            foreach (oxite_PluginSetting setting in pluginSettings)
            {
                settings.Add(setting.PluginSettingName, setting.PluginSettingValue);
            }

            return settings;
        }

        public void Save(IPlugin plugin)
        {
            oxite_Plugin foundPlugin = plugin.ID != Guid.Empty ? (
                from p in context.oxite_Plugins
                where p.SiteID == siteID && p.PluginID == plugin.ID
                select p
                ).FirstOrDefault() : null;

            if (foundPlugin != null)
            {
                foundPlugin.PluginName = plugin.Name;
                foundPlugin.PluginCategory = plugin.Category;
                foundPlugin.Enabled = plugin.Enabled;
                //TODO: (erikpo) Add new fields here
            }
            else
            {
                context.oxite_Plugins.InsertOnSubmit(
                    new oxite_Plugin()
                    {
                        SiteID = siteID,
                        PluginID = plugin.ID != Guid.Empty ? plugin.ID : Guid.NewGuid(),
                        PluginName = plugin.Name,
                        PluginCategory = plugin.Category,
#if DEBUG
                        Enabled = false
#else
                        Enabled = true
#endif
                        //TODO: (erikpo) Add new fields here
                    }
                    );
            }

            if (plugin.Settings != null)
            {
                foreach (string name in plugin.Settings.AllKeys)
                {
                    oxite_PluginSetting setting = getSetting(plugin.ID, name);

                    if (setting != null)
                    {
                        setting.PluginSettingValue = plugin.Settings[name];
                    }
                    else
                    {
                        context.oxite_PluginSettings.InsertOnSubmit(
                            new oxite_PluginSetting()
                            {
                                SiteID = siteID,
                                PluginID = plugin.ID,
                                PluginSettingName = name,
                                PluginSettingValue = plugin.Settings[name]
                            }
                            );
                    }
                }
            }

            context.SubmitChanges();
        }

        private oxite_PluginSetting getSetting(Guid pluginID, string name)
        {
            return (
                from ps in context.oxite_PluginSettings
                where ps.SiteID == siteID && ps.PluginID == pluginID && ps.PluginSettingName == name
                select ps
                ).FirstOrDefault();
        }

        #endregion
    }
}
