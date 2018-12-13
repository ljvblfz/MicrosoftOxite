//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using Oxite.Infrastructure;

namespace Oxite.Plugins.Models
{
    public class PluginProperties : List<ExtendedProperty>
    {
        public PluginProperties(IEnumerable<ExtendedProperty> extendedProperties)
        {
            AddRange(extendedProperties);
        }
    }
}
