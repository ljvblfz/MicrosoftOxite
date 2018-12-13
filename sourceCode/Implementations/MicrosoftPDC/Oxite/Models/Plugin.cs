//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oxite.Extensions;
using Oxite.Infrastructure;
using Oxite.Plugins;

namespace Oxite.Models
{
    public class Plugin : IExtendedPropertyStore
    {
        public Plugin(Guid siteID, string virtualPath, bool enabled, IEnumerable<ExtendedProperty> extendedProperties)
        {
            Site = new SiteSmall(siteID);
            VirtualPath = virtualPath;
            Enabled = enabled;
            ExtendedProperties = extendedProperties;
        }

        public Plugin(Guid siteID, Guid id, string virtualPath, bool enabled, IEnumerable<ExtendedProperty> extendedProperties)
            : this(siteID, virtualPath, enabled, extendedProperties)
        {
            ID = id;
        }

        public Plugin(Guid siteID, Guid id, string virtualPath, bool enabled)
        {
            Site = new SiteSmall(siteID);
            ID = id;
            VirtualPath = virtualPath;
            Enabled = enabled;
        }

        public Plugin(Guid siteID, Guid id, string virtualPath, bool enabled, Exception loadException)
        {
            Site = new SiteSmall(siteID);
            ID = id;
            VirtualPath = virtualPath;
            Enabled = enabled;
        }

        public SiteSmall Site { get; private set; }
        public Guid ID { get; private set; }
        public string VirtualPath { get; private set; }
        public bool Enabled { get; private set; }
        public PluginContainer Container { get; set; }

        public bool GetIsFileWritable()
        {
            return GetIsFileWritable(new HttpContextWrapper(HttpContext.Current));
        }

        public bool GetIsFileWritable(HttpContextBase httpContext)
        {
            return VirtualPath.IsFileWritable(httpContext);
        }

        public string GetFileText()
        {
            return VirtualPath.GetFileText();
        }

        public void SaveFileText(string code)
        {
            VirtualPath.SaveFileText(code);
        }

        public Plugin ApplyProperties(IEnumerable<ExtendedProperty> extendedProperties)
        {
            List<ExtendedProperty> finalExtendedProperties = new List<ExtendedProperty>(extendedProperties);

            foreach (ExtendedProperty extendedProperty in ExtendedProperties)
            {
                ExtendedProperty foundProperty = finalExtendedProperties.FirstOrDefault(ep => string.Compare(ep.Name, extendedProperty.Name, true) == 0 && ep.Type == extendedProperty.Type);

                if (foundProperty != null)
                    foundProperty.Value = extendedProperty.Value;
            }

            return new Plugin(Site.ID, ID, VirtualPath, Enabled, finalExtendedProperties);
        }

        #region IExtendedPropertyStore Members

        public IEnumerable<ExtendedProperty> ExtendedProperties { get; private set; }

        public string ScopeType
        {
            get { return GetType().FullName; }
        }

        public string ScopeKey
        {
            get { return ID.ToString("N"); }
        }

        #endregion
    }
}
