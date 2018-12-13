//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Oxite.Extensions;
using Oxite.Infrastructure;
using Oxite.Models;

namespace Oxite.Modules.Plugins.Repositories.SqlServer
{
    public class SqlServerPluginRepository : IPluginRepository
    {
        private readonly OxitePluginsDataContext context;

        public SqlServerPluginRepository(OxitePluginsDataContext context)
        {
            this.context = context;
        }

        #region IPluginRepository Members

        public IQueryable<Plugin> GetPlugins(Guid siteID)
        {
            return
                getPluginsQuery(siteID)
                .Select(p => projectPlugin(siteID, p));
        }

        public Plugin GetPlugin(Guid siteID, Guid pluginID)
        {
            return
                getPluginsQuery(siteID)
                .Where(p => p.PluginID == pluginID)
                .Select(p => projectPlugin(siteID, p))
                .FirstOrDefault();
        }

        public Plugin Save(Plugin plugin)
        {
            oxite_Plugin pluginToSave = null;

            if (plugin.ID != Guid.Empty)
                pluginToSave = context.oxite_Plugins.FirstOrDefault(p => p.SiteID == plugin.Site.ID && p.PluginID == plugin.ID);

            if (pluginToSave == null)
            {
                pluginToSave = new oxite_Plugin();

                pluginToSave.SiteID = plugin.Site.ID;
                pluginToSave.PluginID = plugin.ID != Guid.Empty ? plugin.ID : Guid.NewGuid();
                pluginToSave.VirtualPath = plugin.VirtualPath;

                context.oxite_Plugins.InsertOnSubmit(pluginToSave);
            }

            pluginToSave.Enabled = plugin.Enabled;

            IExtendedPropertyStore[] extendedPropertiesScope = new IExtendedPropertyStore[] { new ExtendedPropertyStoreBlank(typeof(Plugin).FullName, pluginToSave.PluginID.ToString("N")) };

            //TODO: (erikpo) Change the following to better "sync" the extended properties instead of just wiping them and re-adding them.

            removeExtendedProperties(context, plugin.Site.ID, extendedPropertiesScope);

            context.SubmitChanges();

            foreach (ExtendedProperty extendedProperty in plugin.ExtendedProperties)
                saveExtendedProperties(context, plugin.Site.ID, extendedProperty.Name, extendedProperty.Type, extendedProperty.Value, extendedPropertiesScope);

            context.SubmitChanges();

            return GetPlugin(pluginToSave.SiteID, pluginToSave.PluginID);
        }

        public void SetEnabled(Guid siteID, Guid pluginID, bool enabled)
        {
            oxite_Plugin plugin = context.oxite_Plugins.FirstOrDefault(p => p.SiteID == siteID && p.PluginID == pluginID);

            if (plugin != null)
            {
                plugin.Enabled = enabled;

                context.SubmitChanges();
            }
        }

        public void Remove(Guid siteID, Guid pluginID)
        {
            oxite_Plugin plugin = (
                from p in context.oxite_Plugins
                where p.SiteID == siteID && p.PluginID == pluginID
                select p
                ).FirstOrDefault();

            if (plugin != null)
            {
                removeExtendedProperties(context, siteID, new IExtendedPropertyStore[] { new ExtendedPropertyStoreBlank(typeof(Plugin).FullName, pluginID.ToString("N")) });

                context.oxite_Plugins.DeleteOnSubmit(plugin);

                context.SubmitChanges();
            }
        }

        #endregion

        #region Private Methods

        private Plugin projectPlugin(Guid siteID, oxite_Plugin plugin)
        {
            return new Plugin(plugin.SiteID, plugin.PluginID, plugin.VirtualPath, plugin.Enabled, getExtendedProperties(context, siteID, new IExtendedPropertyStore[] { new ExtendedPropertyStoreBlank(typeof(Plugin).FullName, plugin.PluginID.ToString("N")) }));
        }

        private IQueryable<oxite_Plugin> getPluginsQuery(Guid siteID)
        {
            return context.oxite_Plugins.Where(p => p.SiteID == siteID);
        }

        private static IEnumerable<ExtendedProperty> getExtendedProperties(OxitePluginsDataContext context, Guid siteID, IExtendedPropertyStore[] scopeItems)
        {
            var extendedProperties =
                from ep in context.oxite_ExtendedProperties
                join eps in context.oxite_ExtendedPropertyScopes on ep.ExtendedPropertyID equals eps.ExtendedPropertyID into epg
                join epv in context.oxite_ExtendedPropertyValues on ep.ExtendedPropertyID equals epv.ExtendedPropertyID
                where epv.SiteID == siteID //&& epg.All(eps => scopeItems.Contains(new ExtendedPropertyStoreBlank(eps.ExtendedPropertyScopeType, eps.ExtendedPropertyScopeKey)))
                select new { ExtendedProperty = new { Name = ep.ExtendedPropertyName, Type = epv.ExtendedPropertyType, Value = epv.ExtendedPropertyValue }, ExtendedPropertyScopes = epg };

            //TODO: (erikpo) Find a way to get the scope check into the sql query
            return extendedProperties
                .ToList()
                .Where(ep => ep.ExtendedPropertyScopes.All(eps => scopeItems.Contains(new ExtendedPropertyStoreBlank(eps.ExtendedPropertyScopeType, eps.ExtendedPropertyScopeKey), new ExtendedPropertyStoreComparer())))
                .Select(p => new ExtendedProperty(p.ExtendedProperty.Name, Type.GetType(p.ExtendedProperty.Type), Type.GetType(p.ExtendedProperty.Type).DeserializeValue(p.ExtendedProperty.Value)));
        }

        private static void removeExtendedProperties(OxitePluginsDataContext context, Guid siteID, IExtendedPropertyStore[] scopeItems)
        {
            var extendedProperties =
                from ep in context.oxite_ExtendedProperties
                join eps in context.oxite_ExtendedPropertyScopes on ep.ExtendedPropertyID equals eps.ExtendedPropertyID into epg
                join epv in context.oxite_ExtendedPropertyValues on ep.ExtendedPropertyID equals epv.ExtendedPropertyID
                where epv.SiteID == siteID //&& epg.All(eps => scopeItems.Contains(new ExtendedPropertyStoreBlank(eps.ExtendedPropertyScopeType, eps.ExtendedPropertyScopeKey)))
                select new { ExtendedProperty = ep, ExtendedPropertyScopes = epg };

            //TODO: (erikpo) Find a way to get the scope check into the sql query
            var extendedPropertiesToRemove =
                extendedProperties
                .ToList()
                .Where(ep => ep.ExtendedPropertyScopes.All(eps => scopeItems.Contains(new ExtendedPropertyStoreBlank(eps.ExtendedPropertyScopeType, eps.ExtendedPropertyScopeKey), new ExtendedPropertyStoreComparer())))
                .Select(ep => ep.ExtendedProperty);

            context.oxite_ExtendedPropertyScopes.DeleteAllOnSubmit(
                from eps in context.oxite_ExtendedPropertyScopes
                join ep in context.oxite_ExtendedProperties on eps.ExtendedPropertyID equals ep.ExtendedPropertyID
                where extendedPropertiesToRemove.Contains(ep)
                select eps
                );

            context.oxite_ExtendedPropertyValues.DeleteAllOnSubmit(
                from epv in context.oxite_ExtendedPropertyValues
                join ep in context.oxite_ExtendedProperties on epv.ExtendedPropertyID equals ep.ExtendedPropertyID
                where extendedPropertiesToRemove.Contains(ep)
                select epv
                );

            context.oxite_ExtendedProperties.DeleteAllOnSubmit(extendedPropertiesToRemove);
        }

        private static void saveExtendedProperties(OxitePluginsDataContext context, Guid siteID, string name, Type type, object value, IExtendedPropertyStore[] scopeItems)
        {
            var extendedProperties =
                from ep in context.oxite_ExtendedProperties
                join eps in context.oxite_ExtendedPropertyScopes on ep.ExtendedPropertyID equals eps.ExtendedPropertyID into epg
                join epv in context.oxite_ExtendedPropertyValues on ep.ExtendedPropertyID equals epv.ExtendedPropertyID
                where epv.SiteID == siteID && string.Compare(ep.ExtendedPropertyName, name, true) == 0 //&& epg.All(eps => scopeItems.Contains(new ExtendedPropertyStoreBlank(eps.ExtendedPropertyScopeType, eps.ExtendedPropertyScopeKey)))
                select new { ExtendedProperty = ep, ExtendedPropertyScopes = epg };

            //TODO: (erikpo) Find a way to get the scope check into the sql query
            oxite_ExtendedProperty extendedProperty =
                extendedProperties
                .ToList()
                .Where(ep => ep.ExtendedPropertyScopes.All(eps => scopeItems.Contains(new ExtendedPropertyStoreBlank(eps.ExtendedPropertyScopeType, eps.ExtendedPropertyScopeKey), new ExtendedPropertyStoreComparer())))
                .Select(ep => ep.ExtendedProperty)
                .FirstOrDefault();

            if (extendedProperty != null)
            {
                oxite_ExtendedPropertyValue extendedPropertyValue = context.oxite_ExtendedPropertyValues.First(epv => epv.SiteID == siteID && epv.ExtendedPropertyID == extendedProperty.ExtendedPropertyID);

                extendedPropertyValue.ExtendedPropertyType = type.FullName;
                extendedPropertyValue.ExtendedPropertyValue = type.SerializeValue(value);
            }
            else
            {
                Guid extendedPropertyID = Guid.NewGuid();

                context.oxite_ExtendedProperties.InsertOnSubmit(
                    new oxite_ExtendedProperty
                    {
                        ExtendedPropertyID = extendedPropertyID,
                        ExtendedPropertyName = name
                    }
                    );

                context.oxite_ExtendedPropertyScopes.InsertAllOnSubmit(
                    scopeItems.Select(si =>
                        new oxite_ExtendedPropertyScope
                        {
                            ExtendedPropertyID = extendedPropertyID,
                            ExtendedPropertyScopeType = si.ScopeType,
                            ExtendedPropertyScopeKey = si.ScopeKey
                        }
                        )
                    );

                context.oxite_ExtendedPropertyValues.InsertOnSubmit(
                    new oxite_ExtendedPropertyValue
                    {
                        SiteID = siteID,
                        ExtendedPropertyID = extendedPropertyID,
                        ExtendedPropertyType = type.FullName,
                        ExtendedPropertyValue = type.SerializeValue(value)
                    }
                    );
            }
        }

        #endregion
    }
}
