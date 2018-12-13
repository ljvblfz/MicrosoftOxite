//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Configuration;

namespace Oxite.Infrastructure
{
    public class OxiteConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("instanceName", IsRequired = true)]
        public string InstanceName
        {
            get
            {
                return (string)this["instanceName"];
            }
            set
            {
                this["instanceName"] = value;
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
