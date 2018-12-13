//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using Oxite.Plugins.Models;

namespace Oxite.Plugins.Attributes
{
    public class GroupAttribute : PropertyDefinitionAttribute
    {
        public GroupAttribute(string name)
            : base(new PropertyGroup(name, int.MaxValue))
        {
        }

        public GroupAttribute(string name, int order)
            : base(new PropertyGroup(name, order))
        {
        }
    }
}
