//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------

namespace Oxite.Plugins.Attributes
{
    public class PropertyGroup
    {
        public PropertyGroup(string name, int order)
        {
            Name = name;
            Order = order;
        }

        public string Name { get; private set; }
        public int Order { get; private set; }
    }
}
