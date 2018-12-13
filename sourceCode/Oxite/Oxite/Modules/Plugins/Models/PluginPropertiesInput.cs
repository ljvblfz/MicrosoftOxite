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
using Oxite.Modules.Plugins.Extensions;

namespace Oxite.Modules.Plugins.Models
{
    public class PluginPropertiesInput
    {
        public PluginPropertiesInput(Plugin plugin, NameValueCollection newPropertyValues)
            : this(plugin.Container.Definitions, plugin.ExtendedProperties, name => plugin.GetPropertyDefinition(name), name => newPropertyValues[name])
        {
        }

        public PluginPropertiesInput(IDictionary<string, object> containerDefinitions, IEnumerable<ExtendedProperty> extendedProperties, Func<string, IDictionary<string, object>> getPropertyDefinition, Func<string, object> getPropertyValue)
        {
            ContainerDefinitions = containerDefinitions;
            Properties = new Dictionary<string, KeyValuePair<Type, object>>(extendedProperties.Count());
            PropertyDefinitions = new Dictionary<string, IDictionary<string, object>>(extendedProperties.Count());

            foreach (ExtendedProperty extendedProperty in extendedProperties)
            {
                Properties.Add(extendedProperty.Name, new KeyValuePair<Type, object>(extendedProperties.First(ep => string.Compare(ep.Name, extendedProperty.Name, true) == 0).Type, getPropertyValue(extendedProperty.Name)));

                PropertyDefinitions.Add(extendedProperty.Name, getPropertyDefinition(extendedProperty.Name));
            }
        }

        public IDictionary<string, object> ContainerDefinitions { get; private set; }
        public IDictionary<string, KeyValuePair<Type, object>> Properties { get; private set; }
        public IDictionary<string, IDictionary<string, object>> PropertyDefinitions { get; private set; }
    }
}
