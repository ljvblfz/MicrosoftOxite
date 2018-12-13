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
    public class NumberRangeValidator : IPluginPropertyValidator
    {
        private readonly PluginValidatorRange<byte> byteRange;
        private readonly PluginValidatorRange<short> shortRange;
        private readonly PluginValidatorRange<int> intRange;
        private readonly PluginValidatorRange<long> longRange;
        private readonly PluginValidatorRange<float> floatRange;
        private readonly PluginValidatorRange<double> doubleRange;
        private readonly PluginValidatorRange<decimal> decimalRange;

        private readonly Type[] acceptedTypes = new[]
        {
            typeof(byte),
            typeof(short),
            typeof(int),
            typeof(long),
            typeof(float),
            typeof(double),
            typeof(decimal)
        };

        public NumberRangeValidator(PluginValidatorRange<byte> range)
        {
            byteRange = range;
        }

        public NumberRangeValidator(PluginValidatorRange<short> range)
        {
            shortRange = range;
        }

        public NumberRangeValidator(PluginValidatorRange<int> range)
        {
            intRange = range;
        }

        public NumberRangeValidator(PluginValidatorRange<long> range)
        {
            longRange = range;
        }

        public NumberRangeValidator(PluginValidatorRange<float> range)
        {
            floatRange = range;
        }

        public NumberRangeValidator(PluginValidatorRange<double> range)
        {
            doubleRange = range;
        }

        public NumberRangeValidator(PluginValidatorRange<decimal> range)
        {
            decimalRange = range;
        }

        #region IPluginValidator Members

        public IEnumerable<ValidationError> Validate(string name, Type type, object value)
        {
            if (!acceptedTypes.Contains(type)) throw new InvalidOperationException(string.Format("NumberRangeValidator can only be associated with the following types: {0}", string.Join(", ", acceptedTypes.Select(t => t.FullName).ToArray())));

            List<ValidationError> errors = new List<ValidationError>();

            if (type == typeof(byte))
                validateRange(name, value, errors, () => value is byte ? (byte)value : byte.Parse((string)value), byteRange, (sv, rv) => sv <= rv, (sv, rv) => sv >= rv);
            else if (type == typeof(short))
                validateRange(name, value, errors, () => value is short ? (short)value : short.Parse((string)value), shortRange, (sv, rv) => sv <= rv, (sv, rv) => sv >= rv);
            else if (type == typeof(int))
                validateRange(name, value, errors, () => value is int ? (int)value : int.Parse((string)value), intRange, (sv, rv) => sv <= rv, (sv, rv) => sv >= rv);
            else if (type == typeof(long))
                validateRange(name, value, errors, () => value is long ? (long)value : long.Parse((string)value), longRange, (sv, rv) => sv <= rv, (sv, rv) => sv >= rv);
            else if (type == typeof(float))
                validateRange(name, value, errors, () => value is float ? (float)value : float.Parse((string)value), floatRange, (sv, rv) => sv <= rv, (sv, rv) => sv >= rv);
            else if (type == typeof(double))
                validateRange(name, value, errors, () => value is double ? (double)value : double.Parse((string)value), doubleRange, (sv, rv) => sv <= rv, (sv, rv) => sv >= rv);
            else if (type == typeof(decimal))
                validateRange(name, value, errors, () => value is decimal ? (decimal)value : decimal.Parse((string)value), decimalRange, (sv, rv) => sv <= rv, (sv, rv) => sv >= rv);

            return errors;
        }

        #endregion

        #region Private Methods

        private void validateRange<T>(string name, object value, IList<ValidationError> errors, Func<T> parse, PluginValidatorRange<T> range, Func<T, T, bool> startOutOfRange, Func<T, T, bool> endOutOfRange) where T : struct
        {
            if (range != null)
            {
                T val = parse();

                if (range.Start.HasValue && range.End.HasValue)
                {
                    if (startOutOfRange(val, range.Start.Value) || endOutOfRange(val, range.End.Value))
                        errors.Add(new ValidationError(name, value, string.Format("Plugins.Errors.NumberRangeValidator.Range.{0}", typeof(T).Name), "en", "{0} must be between {1} and {2}", name, range.Start.Value.ToString(), range.End.Value.ToString()));
                }
                else if (range.Start.HasValue)
                {
                    if (startOutOfRange(val, range.Start.Value))
                        errors.Add(new ValidationError(name, value, string.Format("Plugins.Errors.NumberRangeValidator.Start.{0}", typeof(T).Name), "en", "{0} must be greater than or equal to {1}", name, range.Start.Value.ToString()));
                }
                else if (range.End.HasValue)
                {
                    if (endOutOfRange(val, range.End.Value))
                        errors.Add(new ValidationError(name, value, string.Format("Plugins.Errors.NumberRangeValidator.End.{0}", typeof(T).Name), "en", "{0} must be less than or equal to {1}", name, range.End.Value.ToString()));
                }
            }
        }

        #endregion
    }
}
