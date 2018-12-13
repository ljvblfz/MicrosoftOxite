//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using Oxite.Plugins.Validators;

namespace Oxite.Plugins.Attributes
{
    public class Int32RangeValidationAttribute : PropertyDefinitionAttribute
    {
        public Int32RangeValidationAttribute(int start, int end)
            : base(new NumberRangeValidator(new PluginValidatorRange<int>() { Start = start, End = end }), "Validation")
        {
        }
    }
}
