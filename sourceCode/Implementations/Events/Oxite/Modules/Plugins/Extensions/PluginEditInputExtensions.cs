//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using System.Linq;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Plugins.Models;

namespace Oxite.Modules.Plugins.Extensions
{
    public static class PluginEditInputExtensions
    {
        public static Plugin Apply(this Plugin plugin, PluginEditInput input, bool? enabled)
        {
            return new Plugin(plugin.Site.ID, plugin.ID, plugin.VirtualPath, enabled.HasValue ? enabled.Value : input.Enabled.GetValueOrDefault(plugin.Enabled), input.PropertyValues == null ? plugin.ExtendedProperties : input.GetExtendedProperties(plugin.ExtendedProperties));
        }

        public static Plugin ApplyNew(this Plugin plugin, PluginEditInput input, IEnumerable<ExtendedProperty> extendedProperties)
        {
            List<ExtendedProperty> finalExtendedProperties = new List<ExtendedProperty>();

            // remove extended properties that don't exist anymore
            plugin.ExtendedProperties.Where(ep => extendedProperties.Contains(ep, new ExtendedPropertyComparer())).ToList().ForEach(kvp => finalExtendedProperties.Add(kvp));
            // set values for existing extended properties and add new extended properties
            extendedProperties.ToList().ForEach(ep => finalExtendedProperties.First(fep => string.Compare(fep.Name, ep.Name, true) == 0).Value = ep.Value);

            return new Plugin(plugin.Site.ID, plugin.ID, plugin.VirtualPath, input.Enabled.GetValueOrDefault(plugin.Enabled), finalExtendedProperties);
        }
    }
}
