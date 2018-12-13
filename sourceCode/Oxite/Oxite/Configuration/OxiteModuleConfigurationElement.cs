//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Configuration;

namespace Oxite.Configuration
{
    public class OxiteModuleConfigurationElement : ConfigurationElement
    {
        public OxiteModuleConfigurationElement() { }

        public OxiteModuleConfigurationElement(string elementName)
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

        [ConfigurationProperty("dataProvider")]
        public string DataProvider
        {
            get
            {
                return (string)this["dataProvider"];
            }
            set
            {
                this["dataProvider"] = value;
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

        [ConfigurationProperty("settings")]
        public OxiteSettingConfigurationElementCollection Settings
        {
            get
            {
                return (OxiteSettingConfigurationElementCollection)this["settings"];
            }
            set
            {
                this["settings"] = value;
            }
        }
    }
}
