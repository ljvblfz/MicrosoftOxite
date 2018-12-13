//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using Oxite.Validation;

namespace Oxite.Plugins.Validators
{
    public class StringValidator : IPluginPropertyValidator
    {
        public StringValidator()
        {
        }

        public StringValidator(int minLength, int maxLength, RegularExpressionMatcher regularExpressionMatcher)
        {
            MinLength = minLength;
            MaxLength = maxLength;
            RegularExpressionMatcher = regularExpressionMatcher;
        }

        public int MinLength { get; set; }
        public int MaxLength { get; set; }
        public RegularExpressionMatcher RegularExpressionMatcher { get; set; }

        #region IPluginValidator Members

        public IEnumerable<ValidationError> Validate(string name, Type type, object value)
        {
            if (type != typeof(string)) throw new InvalidOperationException(string.Format("StringArrayValidator can only be associated with {0}", typeof(string).FullName));

            List<ValidationError> errors = new List<ValidationError>();

            if (MinLength > 0 && ((string)value).Length < MinLength)
                errors.Add(new ValidationError(name, value, "Plugins.Errors.StringValidator.MinLength", "en", "{0} does not meet the minimum length of {1}", name, MinLength));

            if (MaxLength > 0 && ((string)value).Length > MaxLength)
                errors.Add(new ValidationError(name, value, "Plugins.Errors.StringValidator.MaxLength", "en", "{0} exceeds the maximum length of {1}", name, MaxLength));

            if (RegularExpressionMatcher != null && !RegularExpressionMatcher.RegularExpression.IsMatch((string)value))
                errors.Add(RegularExpressionMatcher.ToValidationError(name, (string)value));

            return errors;
        }

        #endregion
    }
}
