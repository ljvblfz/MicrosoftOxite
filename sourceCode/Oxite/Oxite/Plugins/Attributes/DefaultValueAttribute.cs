//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;

namespace Oxite.Plugins.Attributes
{
    public class DefaultValueAttribute : PropertyDefinitionAttribute
    {
        public DefaultValueAttribute(object value)
            : base(value)
        {
        }
    }
}
