//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using Oxite.Plugins.Validators;

namespace Oxite.Plugins.Attributes
{
    public class Int64RangeValidationAttribute : PropertyDefinitionAttribute
    {
        public Int64RangeValidationAttribute(long start, long end)
            : base(new NumberRangeValidator(new PluginValidatorRange<long>() { Start = start, End = end }), "Validation")
        {
        }
    }
}
