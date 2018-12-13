//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Configuration;

namespace Oxite.Configuration
{
    public class OxiteConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("connectionStrings")]
        public ConnectionStringSettingsCollection ConnectionStrings
        {
            get
            {
                return (ConnectionStringSettingsCollection)this["connectionStrings"];
            }
            set
            {
                this["connectionStrings"] = value;
            }
        }

        [ConfigurationProperty("dataProviders")]
        public OxiteDataProviderConfigurationElementCollection Providers
        {
            get
            {
                return (OxiteDataProviderConfigurationElementCollection)this["dataProviders"];
            }
            set
            {
                this["dataProviders"] = value;
            }
        }

        [ConfigurationProperty("modules")]
        public OxiteModuleConfigurationElementCollection Modules
        {
            get
            {
                return (OxiteModuleConfigurationElementCollection)this["modules"];
            }
            set
            {
                this["modules"] = value;
            }
        }
    }
}
