//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;

namespace Oxite.Configuration.Extensions
{
    public static class OxiteModuleConfigurationElementExtensions
    {
        public static OxiteModuleConfigurationElement Module(this OxiteModuleConfigurationElementCollection modules, Type moduleType)
        {
            foreach (OxiteModuleConfigurationElement element in modules)
                if (element.Type.StartsWith(moduleType.FullName))
                    return element;

            return null;
        }
    }
}