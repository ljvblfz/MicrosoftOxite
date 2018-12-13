//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using Oxite.Plugins.Models;

namespace Oxite.Plugins.Attributes
{
    public class IgnoreAttribute : PropertyDefinitionAttribute
    {
        public IgnoreAttribute()
            : base(true)
        {
        }

        public IgnoreAttribute(bool ignore)
            : base(ignore)
        {
        }
    }
}
