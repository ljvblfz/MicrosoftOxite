//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Oxite.Validation;

namespace Oxite.Plugins.Validators
{
    public class StringArrayValidator : IPluginPropertyValidator
    {
        public StringArrayValidator()
        {
            Separator = ",";
            RemoveEmptyItems = true;
            TrimItemSpaces = true;
        }

        public StringArrayValidator(string separator, bool removeEmptyItems, bool trimItemSpaces, int minCount, int maxCount, int minItemLength, int maxItemLength, RegularExpressionMatcher regularExpressionItemMatcher)
        {
            Separator = separator;
            RemoveEmptyItems = removeEmptyItems;
            TrimItemSpaces = trimItemSpaces;
            MinCount = minCount;
            MaxCount = maxCount;
            MinItemLength = minItemLength;
            MaxItemLength = maxItemLength;
            RegularExpressionItemMatcher = regularExpressionItemMatcher;
        }

        public string Separator { get; set; }
        public bool RemoveEmptyItems { get; set; }
        public bool TrimItemSpaces { get; set; }
        public int MinCount { get; set; }
        public int MaxCount { get; set; }
        public int MinItemLength { get; set; }
        public int MaxItemLength { get; set; }
        public RegularExpressionMatcher RegularExpressionItemMatcher { get; set; }

        #region IPluginValidator Members

        public IEnumerable<ValidationError> Validate(string name, Type type, object value)
        {
            if (type != typeof(string[])) throw new InvalidOperationException(string.Format("StringArrayValidator can only be associated with {0}", typeof(string[]).FullName));

            List<ValidationError> errors = new List<ValidationError>();
            string[] values = value is string[] ? (string[])value : ((string)value).Split(new[] { Separator }, RemoveEmptyItems ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None);

            if (TrimItemSpaces)
                values = values.Select(s => s.Trim()).ToArray();

            if (MinCount > 0 && values.Length < MinCount)
                errors.Add(new ValidationError(name, values, "Plugins.Errors.StringArrayValidator.MinCount", "en", "{0} does not contain enough items to meet the minimum of {1}", name, MinCount));

            if (MaxCount > 0 && values.Length > MaxCount)
                errors.Add(new ValidationError(name, values, "Plugins.Errors.StringArrayValidator.MaxCount", "en", "{0} contains too many items and exceeds the maximum of {1}", name, MaxCount));

            if (MinItemLength > 0 || MaxItemLength > 0 || RegularExpressionItemMatcher != null)
            {
                foreach (string item in values)
                {
                    if (MinItemLength > 0 && item.Length < MinItemLength)
                        errors.Add(new ValidationError(name, item, "Plugins.Errors.StringArrayValidator.MinItemLength", "en", "{0} contains item {1} that does not meet the minimum length of {2}", name, item, MinItemLength));

                    if (MaxItemLength > 0 && item.Length > MaxItemLength)
                        errors.Add(new ValidationError(name, item, "Plugins.Errors.StringArrayValidator.MaxItemLength", "en", "{0} contains item {1} that exceeds the maximum length of {2}", name, item, MaxItemLength));

                    if (RegularExpressionItemMatcher != null && !RegularExpressionItemMatcher.RegularExpression.IsMatch(item))
                        errors.Add(RegularExpressionItemMatcher.ToValidationError(name, item));
                }
            }

            return errors;
        }

        #endregion
    }
}
