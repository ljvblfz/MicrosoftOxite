//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using Oxite.Plugins.Validators;

namespace Oxite.Plugins.Attributes
{
    public class StringValidationAttribute : PropertyDefinitionAttribute
    {
        public StringValidationAttribute()
            : base(null, "Validation")
        {
        }

        public int MinLength { get; set; }
        public int MaxLength { get; set; }
        public RegularExpressionMatcher RegularExpressionMatcher { get; set; }

        protected override void EnsureValue()
        {
            Value = new StringValidator(MinLength, MaxLength, RegularExpressionMatcher);
        }
    }
}
