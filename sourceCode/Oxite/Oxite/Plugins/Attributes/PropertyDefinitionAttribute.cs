//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;

namespace Oxite.Plugins.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyDefinitionAttribute : Attribute
    {
        public PropertyDefinitionAttribute(object value)
        {
            Value = value;
        }

        public PropertyDefinitionAttribute(object value, string name)
            : this(value)
        {
            Name = name;
        }

        public string Name { get; private set; }

        private object value;
        public object Value
        {
            get
            {
                if (value == null)
                    EnsureValue();

                return value;
            }
            protected set
            {
                this.value = value;
            }
        }

        protected virtual void EnsureValue()
        {
        }
    }
}
