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

namespace Oxite.Repositories.SqlServer
{
    public class SqlServerExtendedPropertyRepository : IExtendedPropertyRepository
    {
        private readonly OxiteDataContext context;

        public SqlServerExtendedPropertyRepository(OxiteDataContext context)
        {
            this.context = context;
        }

        #region IExtendedPropertyRepository Members

        public IEnumerable<ExtendedProperty> GetExtendedProperties(Guid siteID, IExtendedPropertyStore[] scopeItems)
        {
            return GetExtendedPropertiesInternal(context, siteID, scopeItems);
        }

        internal static IEnumerable<ExtendedProperty> GetExtendedPropertiesInternal(OxiteDataContext context, Guid siteID, IExtendedPropertyStore[] scopeItems)
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

        public void Remove(Guid siteID, IExtendedPropertyStore[] scopeItems)
        {
            RemoveInternal(context, siteID, scopeItems);

            context.SubmitChanges();
        }

        internal static void RemoveInternal(OxiteDataContext context, Guid siteID, IExtendedPropertyStore[] scopeItems)
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

        public void Save(Guid siteID, string name, Type type, object value, IExtendedPropertyStore[] scopeItems)
        {
            SaveInternal(context, siteID, name, type, value, scopeItems);

            context.SubmitChanges();
        }

        internal static void SaveInternal(OxiteDataContext context, Guid siteID, string name, Type type, object value, IExtendedPropertyStore[] scopeItems)
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
