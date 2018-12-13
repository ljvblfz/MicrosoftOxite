//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Specialized;

namespace Oxite.Configuration.Extensions
{
    public static class OxiteSettingConfigurationElementCollectionExtensions
    {
        public static NameValueCollection ToNameValueCollection(this OxiteSettingConfigurationElementCollection settings)
        {
            NameValueCollection nvm = new NameValueCollection(settings.Count);

            foreach (OxiteSettingConfigurationElement setting in settings)
                nvm.Add(setting.Name, setting.Value);

            return nvm;
        }
    }
}