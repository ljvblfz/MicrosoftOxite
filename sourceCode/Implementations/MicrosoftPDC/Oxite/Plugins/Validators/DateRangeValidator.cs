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
    public class DateRangeValidator : IPluginPropertyValidator
    {
        private PluginValidatorRange<DateTime> range;

        public DateRangeValidator(PluginValidatorRange<DateTime> range)
        {
            this.range = range;
        }

        #region IPluginValidator Members

        public IEnumerable<ValidationError> Validate(string name, Type type, object value)
        {
            if (type != typeof(DateTime)) throw new InvalidOperationException(string.Format("DateRangeValidator can only be associated with {0}", typeof(DateTime).FullName));

            List<ValidationError> errors = new List<ValidationError>();

            if (range != null)
            {
                DateTime dateValue = value is DateTime ? (DateTime)value : DateTime.Parse((string)value);

                if (range.Start.HasValue && range.End.HasValue)
                {
                    if (dateValue < range.Start.Value || dateValue > range.End.Value)
                        errors.Add(new ValidationError(name, value, "Plugins.Errors.DateRangeValidator.Range", "en", "{0} must be between '{1}' and '{2}'", name, range.Start.Value.ToShortDateString(), range.End.Value.ToShortDateString()));
                }
                else if (range.Start.HasValue)
                {
                    if (dateValue < range.Start.Value)
                        errors.Add(new ValidationError(name, value, "Plugins.Errors.DateRangeValidator.Start", "en", "{0} must be after '{1}'", name, range.Start.Value.ToShortDateString()));
                }
                else if (range.End.HasValue)
                {
                    if (dateValue > range.End.Value)
                        errors.Add(new ValidationError(name, value, "Plugins.Errors.DateRangeValidator.End", "en", "{0} must be before '{1}'", name, range.End.Value.ToShortDateString()));
                }
            }

            return errors;
        }

        #endregion
    }
}
