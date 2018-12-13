//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;

namespace Oxite.Plugins.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class DefinitionAttribute : Attribute
    {
        public DefinitionAttribute(object value)
        {
            Value = value;
        }

        public string NameOverride { get; protected set; }
        public object Value { get; private set; }
    }
}
