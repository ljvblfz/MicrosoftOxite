//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using Oxite.Plugins.Validators;

namespace Oxite.Plugins.Attributes
{
    public class ByteRangeValidationAttribute : PropertyDefinitionAttribute
    {
        public ByteRangeValidationAttribute(byte start, byte end)
            : base(new NumberRangeValidator(new PluginValidatorRange<byte>() { Start = start, End = end }), "Validation")
        {
        }
    }
}
