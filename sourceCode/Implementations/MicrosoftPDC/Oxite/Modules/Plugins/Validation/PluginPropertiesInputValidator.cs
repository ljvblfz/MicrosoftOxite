//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using Oxite.Extensions;
using Oxite.Infrastructure;
using Oxite.Modules.Plugins.Models;
using Oxite.Plugins.Validators;
using Oxite.Services;
using Oxite.Validation;

namespace Oxite.Modules.Plugins.Validation
{
    public class PluginPropertiesInputValidator : ValidatorBase<PluginPropertiesInput>
    {
        public PluginPropertiesInputValidator(ILocalizationService localizationService, IRegularExpressions expressions, OxiteContext context)
            : base(localizationService, expressions, context) { }

        #region IValidator Members

        public override ValidationState Validate(PluginPropertiesInput pluginPropertiesInput)
        {
            if (pluginPropertiesInput == null) throw new ArgumentNullException("pluginPropertiesInput");

            ValidationState validationState = new ValidationState();

            foreach (string key in pluginPropertiesInput.PropertyDefinitions.Keys)
            {
                IDictionary<string, object> propertyDefinitions = pluginPropertiesInput.PropertyDefinitions[key];
                Type propertyType = pluginPropertiesInput.Properties[key].Key;
                object propertyValue = pluginPropertiesInput.Properties[key].Value;

                //TODO: (erikpo) Need to move all the else/if into Visitor classes

                if (propertyValue != null && propertyValue is string)
                    propertyValue = ((string)propertyValue).Trim();

                if (propertyType == typeof(string))
                {
                    if (isValidFromRequiredCheck(key, propertyValue, validationState, propertyDefinitions, () => propertyValue != null && (string)propertyValue != ""))
                    {
                        executePropertyValidator<StringValidator>(key, propertyType, propertyValue, propertyDefinitions, validationState);
                    }
                }
                else if (propertyType == typeof(string[]))
                {
                    StringArrayValidator validator = propertyDefinitions.GetPluginPropertyValidator<StringArrayValidator>();

                    if (isValidFromRequiredCheck(key, propertyValue, validationState, propertyDefinitions, () => propertyValue != null && (propertyValue is string[] && ((string)propertyValue).Length > 0)) || (propertyValue is string && ((string)propertyValue).Split(new string[] { validator != null ? validator.Separator : "," }, StringSplitOptions.RemoveEmptyEntries).Length > 0))
                    {
                        if (validator != null)
                            validationState.Errors.AddRange(validator.Validate(key, propertyType, propertyValue));
                    }
                }
                else if (propertyType == typeof(bool) || propertyType == typeof(bool?))
                {
                    if (propertyType == typeof(bool) || isValidFromRequiredCheck(key, propertyValue, validationState, propertyDefinitions, checkProperty(propertyValue)))
                    {
                        bool boolValue;
                        if (propertyValue is string && !bool.TryParse((string)propertyValue, out boolValue))
                            validationState.Errors.Add(createUnableToParseValidationError(key, "boolean", propertyValue));
                    }
                }
                else if (propertyType == typeof(byte) || propertyType == typeof(byte?))
                {
                    if (propertyType == typeof(byte) || isValidFromRequiredCheck(key, propertyValue, validationState, propertyDefinitions, checkProperty(propertyValue)))
                    {
                        byte byteValue;
                        if (propertyValue is string && !byte.TryParse((string)propertyValue, out byteValue))
                            validationState.Errors.Add(createUnableToParseValidationError(key, string.Format("number between {0} and {1}", byte.MinValue, byte.MaxValue), propertyValue));
                        else
                            executePropertyValidator<NumberRangeValidator>(key, propertyType, propertyValue, propertyDefinitions, validationState);
                    }
                }
                else if (propertyType == typeof(short) || propertyType == typeof(short?))
                {
                    if (propertyType == typeof(short) || isValidFromRequiredCheck(key, propertyValue, validationState, propertyDefinitions, checkProperty(propertyValue)))
                    {
                        short shortValue;
                        if (propertyValue is string && !short.TryParse((string)propertyValue, out shortValue))
                            validationState.Errors.Add(createUnableToParseValidationError(key, string.Format("number between {0} and {1}", short.MinValue, short.MaxValue), propertyValue));
                        else
                            executePropertyValidator<NumberRangeValidator>(key, propertyType, propertyValue, propertyDefinitions, validationState);
                    }
                }
                else if (propertyType == typeof(int) || propertyType == typeof(int?))
                {
                    if (propertyType == typeof(int) || isValidFromRequiredCheck(key, propertyValue, validationState, propertyDefinitions, checkProperty(propertyValue)))
                    {
                        int intValue;
                        if (propertyValue is string && !int.TryParse((string)propertyValue, out intValue))
                            validationState.Errors.Add(createUnableToParseValidationError(key, string.Format("number between {0} and {1}", int.MinValue, int.MaxValue), propertyValue));
                        else
                            executePropertyValidator<NumberRangeValidator>(key, propertyType, propertyValue, propertyDefinitions, validationState);
                    }
                }
                else if (propertyType == typeof(long) || propertyType == typeof(long))
                {
                    if (propertyType == typeof(long) || isValidFromRequiredCheck(key, propertyValue, validationState, propertyDefinitions, checkProperty(propertyValue)))
                    {
                        long longValue;
                        if (propertyValue is string && !long.TryParse((string)propertyValue, out longValue))
                            validationState.Errors.Add(createUnableToParseValidationError(key, string.Format("number between {0} and {1}", long.MinValue, long.MaxValue), propertyValue));
                        else
                            executePropertyValidator<NumberRangeValidator>(key, propertyType, propertyValue, propertyDefinitions, validationState);
                    }
                }
                else if (propertyType == typeof(float) || propertyType == typeof(float?))
                {
                    if (propertyType == typeof(float) || isValidFromRequiredCheck(key, propertyValue, validationState, propertyDefinitions, checkProperty(propertyValue)))
                    {
                        float floatValue;
                        if (propertyValue is string && !float.TryParse((string)propertyValue, out floatValue))
                            validationState.Errors.Add(createUnableToParseValidationError(key, "decimal", propertyValue));
                        else
                            executePropertyValidator<NumberRangeValidator>(key, propertyType, propertyValue, propertyDefinitions, validationState);
                    }
                }
                else if (propertyType == typeof(double) || propertyType == typeof(double?))
                {
                    if (propertyType == typeof(double) || isValidFromRequiredCheck(key, propertyValue, validationState, propertyDefinitions, checkProperty(propertyValue)))
                    {
                        double doubleValue;
                        if (propertyValue is string && !double.TryParse((string)propertyValue, out doubleValue))
                            validationState.Errors.Add(createUnableToParseValidationError(key, "decimal", propertyValue));
                        else
                            executePropertyValidator<NumberRangeValidator>(key, propertyType, propertyValue, propertyDefinitions, validationState);
                    }
                }
                else if (propertyType == typeof(decimal) || propertyType == typeof(decimal?))
                {
                    if (propertyType == typeof(decimal) || isValidFromRequiredCheck(key, propertyValue, validationState, propertyDefinitions, checkProperty(propertyValue)))
                    {
                        decimal decimalValue;
                        if (propertyValue is string && !decimal.TryParse((string)propertyValue, out decimalValue))
                            validationState.Errors.Add(createUnableToParseValidationError(key, "decimal", propertyValue));
                        else
                            executePropertyValidator<NumberRangeValidator>(key, propertyType, propertyValue, propertyDefinitions, validationState);
                    }
                }
                else if (propertyType == typeof(DateTime) || propertyType == typeof(DateTime?))
                {
                    if (propertyType == typeof(DateTime) || isValidFromRequiredCheck(key, propertyValue, validationState, propertyDefinitions, checkProperty(propertyValue)))
                    {
                        DateTime dateTimeValue;
                        if (propertyValue is string && !DateTime.TryParse((string)propertyValue, out dateTimeValue))
                            validationState.Errors.Add(createUnableToParseValidationError(key, "date/time", propertyValue));
                        else
                            executePropertyValidator<DateRangeValidator>(key, propertyType, propertyValue, propertyDefinitions, validationState);
                    }
                }
                else if (propertyType == typeof(Guid) || propertyType == typeof(Guid?))
                {
                    if (propertyType == typeof(Guid) || isValidFromRequiredCheck(key, propertyValue, validationState, propertyDefinitions, checkProperty(propertyValue)))
                    {
                        if (propertyValue is string)
                        {
                            try
                            {
                                new Guid((string)propertyValue);
                            }
                            catch
                            {
                                validationState.Errors.Add(createUnableToParseValidationError(key, "globally unique identifier (GUID)", propertyValue));
                            }
                        }
                    }
                }
                else
                {
                    executePropertyValidator<IPluginPropertyValidator>(key, propertyType, propertyValue, propertyDefinitions, validationState);
                }
            }

            executeValidator<IPluginValidator>(pluginPropertiesInput, validationState);

            return validationState;
        }

        #endregion

        #region Private Methods

        private bool isValidFromRequiredCheck(string propertyName, object propertyValue, ValidationState validationState, IDictionary<string, object> propertyDefinitions, Func<bool> hasValue)
        {
            if (propertyDefinitions.ContainsKey("Required") && (bool)propertyDefinitions["Required"] && !hasValue())
            {
                validationState.Errors.Add(CreateValidationError(propertyValue, propertyName, "Plugins.Errors.Generic.Required", "{0} is required and no value was provided", propertyName));

                return false;
            }

            return true;
        }

        private void executeValidator<T>(PluginPropertiesInput pluginPropertiesInput, ValidationState validationState) where T : class, IPluginValidator
        {
            T validator = pluginPropertiesInput.ContainerDefinitions.GetPluginValidator<T>();

            if (validator != null)
                validationState.Errors.AddRange(validator.Validate(pluginPropertiesInput.Properties));
        }

        private void executePropertyValidator<T>(string propertyName, Type propertyType, object propertyValue, IDictionary<string, object> propertyDefinitions, ValidationState validationState) where T : class, IPluginPropertyValidator
        {
            T validator = propertyDefinitions.GetPluginPropertyValidator<T>();

            if (validator != null)
                validationState.Errors.AddRange(validator.Validate(propertyName, propertyType, propertyValue));
        }

        private ValidationError createUnableToParseValidationError(string propertyName, string propertyTypeName, object propertyValue)
        {
            return CreateValidationError(propertyValue, propertyName, "Plugins.Errors.Generic.UnableToParse", "{0} was not a valid {1}", propertyName, propertyTypeName);
        }

        private Func<bool> checkProperty(object propertyValue)
        {
            return () => propertyValue != null && (propertyValue is bool || (propertyValue is string && (string)propertyValue != ""));
        }

        #endregion
    }
}
