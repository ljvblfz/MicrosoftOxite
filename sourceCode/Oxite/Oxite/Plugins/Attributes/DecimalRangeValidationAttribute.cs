//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using Oxite.Plugins.Validators;

namespace Oxite.Plugins.Attributes
{
    public class DecimalRangeValidationAttribute : PropertyDefinitionAttribute
    {
        public DecimalRangeValidationAttribute(decimal start, decimal end)
            : base(new NumberRangeValidator(new PluginValidatorRange<decimal>() { Start = start, End = end }), "Validation")
        {
        }
    }
}
