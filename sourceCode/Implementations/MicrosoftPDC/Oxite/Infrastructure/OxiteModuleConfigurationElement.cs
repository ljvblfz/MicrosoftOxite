//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Configuration;
using System.Reflection;

namespace Oxite.Infrastructure
{
    public class OxiteModuleConfigurationElement : ConfigurationElement
    {
        public OxiteModuleConfigurationElement() : base() { }

        public OxiteModuleConfigurationElement(string elementName)
            : base()
        {
            Name = elementName;
        }

        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }
            set
            {
                this["name"] = value;
            }
        }

        [ConfigurationProperty("type", IsRequired = true)]
        public string Type
        {
            get
            {
                return (string)this["type"];
            }
            set
            {
                this["type"] = value;
            }
        }

        [ConfigurationProperty("enabled", IsRequired = false, DefaultValue = true)]
        public bool Enabled
        {
            get
            {
                return (bool)this["enabled"];
            }
            set
            {
                this["enabled"] = value;
            }
        }

        [ConfigurationProperty("properties")]
        public new OxiteModulePropertyConfigurationElementCollection Properties
        {
            get
            {
                return (OxiteModulePropertyConfigurationElementCollection)this["properties"];
            }
            set
            {
                this["properties"] = value;
            }
        }
    }
}
