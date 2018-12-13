//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Text.RegularExpressions;
using Oxite.Validation;

namespace Oxite.Plugins.Validators
{
    public class RegularExpressionMatcher
    {
        public RegularExpressionMatcher(Regex regularExpression)
        {
            RegularExpression = regularExpression;
        }

        public RegularExpressionMatcher(Regex regularExpression, string message)
            : this(regularExpression)
        {
            Message = message;
        }

        public RegularExpressionMatcher(Regex regularExpression, string message, string messageKey, string messageLanguage, params object[] messageValues)
            : this(regularExpression, message)
        {
            MessageKey = messageKey;
            MessageLanguage = messageLanguage;
            MessageValues = messageValues;
        }

        public Regex RegularExpression { get; private set; }
        public string Message { get; private set; }
        public string MessageKey { get; private set; }
        public string MessageLanguage { get; private set; }
        public object[] MessageValues { get; private set; }

        public ValidationError ToValidationError(string settingName, string settingValue)
        {
            if (!string.IsNullOrEmpty(MessageKey))
                return new ValidationError(settingName, settingValue, MessageKey, MessageLanguage, Message, MessageValues);
            
            if (!string.IsNullOrEmpty(Message))
                return new ValidationError(settingName, settingValue, Message);
            
            return new ValidationError(settingName, settingValue, "Plugins.Errors.Generic.RegularExpression", "en", "Plugin setting {0} could not be validated", settingName);
        }
    }
}
