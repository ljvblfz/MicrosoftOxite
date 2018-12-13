//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Configuration;

namespace Oxite.Configuration
{
    public class OxiteDataProviderConfigurationElementCollection : ConfigurationElementCollection
    {
        [ConfigurationProperty("defaultConnectionString")]
        public string DefaultConnectionString
        {
            get
            {
                return (string)this["defaultConnectionString"];
            }
            set
            {
                this["defaultConnectionString"] = value;
            }
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new OxiteDataProviderConfigurationElement();
        }

        protected override ConfigurationElement CreateNewElement(string elementName)
        {
            return new OxiteDataProviderConfigurationElement(elementName);
        }

        protected new OxiteDataProviderConfigurationElement BaseGet(int index)
        {
            return (OxiteDataProviderConfigurationElement)base.BaseGet(index);
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((OxiteDataProviderConfigurationElement)element).Name;
        }
    }
}
