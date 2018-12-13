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
using Oxite.BackgroundServices;

namespace Oxite.LinqToSqlDataProvider
{
    public class BackgroundServiceRepository : IBackgroundServiceRepository
    {
        private OxiteLinqToSqlDataContext context;
        private Guid siteID;

        public BackgroundServiceRepository(OxiteLinqToSqlDataContext context, Site site)
        {
            this.context = context;
            this.siteID = site.ID;
        }

        #region IPluginRepository Members

        public IList<IBackgroundService> GetBackgroundServices()
        {
            var query =
                from p in context.oxite_Plugins
                where p.SiteID == siteID
                orderby p.PluginCategory/*, p.PluginDisplayName*/, p.PluginName
                select p;

            return query.Select(
                p =>
                    new BackgroundServiceBase()
                    {
                        ID = p.PluginID,
                        Name = p.PluginName,
                        Category = p.PluginCategory,
                        //DisplayName = p.PluginDisplayName,
                        Enabled = p.Enabled
                    }
                ).Cast<IBackgroundService>().ToList();
        }

        ////TODO: (erikpo) Not sure if this code should be broken up or where if it should live somewhere else.
        //public IList<Plugin> GetPluginsNotInstalled()
        //{
        //    IList<Plugin> pluginsInstalled = GetPlugins();
        //    //TODO: (erikpo) Change Plugins path to not be hardcoded
        //    string pluginsFilePath = System.Web.HttpContext.Current.Server.MapPath("/Plugins");
        //    List<Plugin> pluginsNotInstalled = new List<Plugin>(10);

        //    foreach (string categoryDir in System.IO.Directory.GetDirectories(pluginsFilePath))
        //    {
        //        string categoryName = categoryDir.Remove(0, System.IO.Path.GetDirectoryName(categoryDir).Length + 1);

        //        foreach (string pluginDir in System.IO.Directory.GetDirectories(categoryDir))
        //        {
        //            string pluginName = pluginDir.Remove(0, System.IO.Path.GetDirectoryName(pluginDir).Length + 1);
        //            string pluginBinDir = System.IO.Path.Combine(pluginDir, "bin");

        //            if (System.IO.Directory.Exists(pluginBinDir))
        //            {
        //                string expectedAssemblyPath = System.IO.Path.Combine(pluginBinDir, pluginName + ".dll");

        //                if (System.IO.File.Exists(expectedAssemblyPath) && !pluginsInstalled.Where(p => string.Compare(p.Category, categoryName, true) == 0 && string.Compare(p.Name, pluginName, true) == 0).Any())
        //                {
        //                    pluginsNotInstalled.Add(new Plugin(Guid.Empty, pluginName, categoryName));
        //                }
        //            }
        //        }
        //    }

        //    return pluginsNotInstalled;
        //}

        public IBackgroundService GetBackgroundService(Guid backgroundServiceID)
        {
            return (
                from p in context.oxite_Plugins
                let pluginSettings = getPluginSettings(p)
                where p.SiteID == siteID && p.PluginID == backgroundServiceID
                select new BackgroundServiceBase()
                {
                    ID = p.PluginID,
                    Name = p.PluginName,
                    Category = p.PluginCategory,
                    Enabled = p.Enabled,
                    Settings = projectPluginSettings(pluginSettings)
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

        public bool GetBackgroundServiceExists(Guid backgroundServiceID)
        {
            return (
                from p in context.oxite_Plugins
                where p.SiteID == siteID && p.PluginID == backgroundServiceID
                select p
                ).FirstOrDefault() != null;
        }

        public void Save(IBackgroundService backgroundService)
        {
            Save(backgroundService, null);
        }

        public void Save(IBackgroundService backgroundService, NameValueCollection settings)
        {
            oxite_Plugin foundPlugin = backgroundService.ID != Guid.Empty ? (
                from p in context.oxite_Plugins
                where p.SiteID == siteID && p.PluginID == backgroundService.ID
                select p
                ).FirstOrDefault() : null;

            if (foundPlugin != null)
            {
                foundPlugin.PluginName = backgroundService.Name;
                foundPlugin.PluginCategory = backgroundService.Category;
                foundPlugin.Enabled = backgroundService.Enabled;
                //TODO: (erikpo) Add new fields here
            }
            else
            {
                context.oxite_Plugins.InsertOnSubmit(
                    new oxite_Plugin()
                    {
                        SiteID = siteID,
                        PluginID = backgroundService.ID != Guid.Empty ? backgroundService.ID : Guid.NewGuid(),
                        PluginName = backgroundService.Name,
                        PluginCategory = backgroundService.Category,
                        Enabled = backgroundService.Enabled
                        //TODO: (erikpo) Add new fields here
                    }
                    );
            }

            if (settings != null)
            {
                foreach (string name in settings.AllKeys)
                {
                    oxite_PluginSetting setting = getSetting(backgroundService.ID, name);

                    if (setting != null)
                    {
                        setting.PluginSettingValue = settings[name];
                    }
                    else
                    {
                        context.oxite_PluginSettings.InsertOnSubmit(
                            new oxite_PluginSetting()
                            {
                                SiteID = siteID,
                                PluginID = backgroundService.ID,
                                PluginSettingName = name,
                                PluginSettingValue = settings[name]
                            }
                            );
                    }
                }

                //TODO: (erikpo) Cleanup settings that aren't needed anymore
            }

            context.SubmitChanges();
        }

        public NameValueCollection GetBackgroundServiceSettings(Guid backgroundServiceID)
        {
            var query =
                from ps in context.oxite_PluginSettings
                where ps.SiteID == siteID && ps.PluginID == backgroundServiceID
                select new { Name = ps.PluginSettingName, Value = ps.PluginSettingValue };

            NameValueCollection settings = new NameValueCollection(query.Count());

            foreach (var item in query)
            {
                settings.Add(item.Name, item.Value);
            }

            return settings;
        }

        public void SaveSetting(Guid backgroundServiceID, string name, string value)
        {
            if (backgroundServiceID == Guid.Empty) throw new ArgumentException("Guid.Empty is not a valid value", "pluginID");
            if (value == null) throw new ArgumentNullException("value");

            oxite_PluginSetting setting = getSetting(backgroundServiceID, name);

            if (setting != null)
            {
                setting.PluginSettingValue = value;
            }
            else
            {
                context.oxite_PluginSettings.InsertOnSubmit(
                    new oxite_PluginSetting()
                    {
                        SiteID = siteID,
                        PluginID = backgroundServiceID,
                        PluginSettingName = name,
                        PluginSettingValue = value
                    }
                    );
            }

            context.SubmitChanges();
        }

        private oxite_PluginSetting getSetting(Guid backgroundServiceID, string name)
        {
            return (
                from ps in context.oxite_PluginSettings
                where ps.SiteID == siteID && ps.PluginID == backgroundServiceID && ps.PluginSettingName == name
                select ps
                ).FirstOrDefault();
        }

        #endregion
    }
}
