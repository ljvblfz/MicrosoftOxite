//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Routing;
using Oxite.Plugins.Attributes;
using Oxite.Plugins.Models;

namespace Oxite.Plugins
{
    public class PluginContainer : PluginAssemblyContainer
    {
        private IDictionary<string, object> definitions;
        private IDictionary<string, IDictionary<string, object>> propertyDefinitions;

        public PluginContainer(PluginAssemblyContainer assemblyContainer, Exception pluginLoadException)
            : base(assemblyContainer.VirtualPath, pluginLoadException)
        {
        }

        public PluginContainer(PluginAssemblyContainer assemblyContainer, object instance)
            : base(assemblyContainer.VirtualPath, assemblyContainer.CompilationAssembly)
        {
            Instance = instance;
        }

        public bool IsValid { get; set; }
        public object Instance { get; private set; }
        public object Tag { get; set; }
        public IDictionary<string, object> Definitions
        {
            get
            {
                if (definitions == null)
                    definitions = loadDefinitions(Instance);

                return definitions;
            }
        }

        public IDictionary<string, object> GetPropertyDefinitions(string propertyName)
        {
            if (propertyDefinitions == null)
                propertyDefinitions = new Dictionary<string, IDictionary<string, object>>(10);

            if (propertyDefinitions.ContainsKey(propertyName)) return propertyDefinitions[propertyName];

            IDictionary<string, object> definitions = new Dictionary<string, object>();

            if (Instance != null)
            {
                Type pluginType = Instance.GetType();
                PropertyInfo property = pluginType.GetProperties(BindingFlags.Public | BindingFlags.Instance).FirstOrDefault(p => p.Name == propertyName);
                PropertyInfo propertyDefinition = pluginType.GetProperties(BindingFlags.Public | BindingFlags.Instance).FirstOrDefault(p => p.Name == propertyName + "Definition");
                PropertyDefinitionAttribute[] attributes = (PropertyDefinitionAttribute[])property.GetCustomAttributes(typeof(PropertyDefinitionAttribute), true);

                if (propertyDefinition != null)
                {
                    object instancePropertyDefinition = propertyDefinition.GetValue(Instance, null);

                    if (instancePropertyDefinition != null)
                        definitions = new Dictionary<string, object>(new RouteValueDictionary(instancePropertyDefinition));
                }

                foreach (PropertyDefinitionAttribute attribute in attributes)
                {
                    string key;

                    if (!string.IsNullOrEmpty(attribute.Name))
                        key = attribute.Name;
                    else
                    {
                        key = attribute.GetType().Name;
                        key = key.Substring(0, key.Length - "Attribute".Length);
                    }

                    if (definitions.ContainsKey(key))
                        definitions[key] = attribute.Value;
                    else
                        definitions.Add(key, attribute.Value);
                }
            }

            propertyDefinitions.Add(propertyName, definitions);

            return definitions;
        }

        private static IDictionary<string, object> loadDefinitions(object instance)
        {
            if (instance != null)
            {
                Type pluginType = instance.GetType();
                PropertyInfo property = pluginType.GetProperties(BindingFlags.Public | BindingFlags.Instance).FirstOrDefault(p => p.Name == "Definition");
                DefinitionAttribute[] attributes = (DefinitionAttribute[])pluginType.GetCustomAttributes(typeof(DefinitionAttribute), true);
                IDictionary<string, object> definitions = null;

                if (property != null)
                {
                    object instanceDefinitions = property.GetValue(instance, null);

                    if (instanceDefinitions != null)
                        definitions = new Dictionary<string, object>(new RouteValueDictionary(instanceDefinitions));
                }

                if (definitions == null)
                    definitions = new Dictionary<string, object>();

                foreach (DefinitionAttribute attribute in attributes)
                {
                    string key;

                    if (!string.IsNullOrEmpty(attribute.NameOverride))
                        key = attribute.NameOverride;
                    else
                    {
                        key = attribute.GetType().Name;
                        key = key.Substring(0, key.Length - "Attribute".Length);
                    }

                    if (definitions.ContainsKey(key))
                        definitions[key] = attribute.Value;
                    else
                        definitions.Add(key, attribute.Value);
                }

                return definitions;
            }

            return new Dictionary<string, object>();
        }
    }
}
