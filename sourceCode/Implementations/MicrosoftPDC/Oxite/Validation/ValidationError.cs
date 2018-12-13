//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;

namespace Oxite.Validation
{
    public class ValidationError
    {
        public ValidationError(string name, object attemptedValue, string message)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");
            if (string.IsNullOrEmpty(message)) throw new ArgumentNullException("message");

            Name = name;
            AttemptedValue = attemptedValue;
            Message = message;
            Exception = new Exception(message);
        }

        public ValidationError(string name, object attemptedValue, Exception exception)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");
            if (exception == null) throw new ArgumentNullException("exception");

            Name = name;
            AttemptedValue = attemptedValue;
            Exception = exception;
            Message = exception.Message;
        }

        public ValidationError(string name, object attemptedValue, string messageKey, string messageLanguage, string message, params object[] messageValues)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");
            if (string.IsNullOrEmpty(messageKey)) throw new ArgumentNullException("messageKey");
            if (string.IsNullOrEmpty(messageLanguage)) throw new ArgumentNullException("messageLanguage");
            if (string.IsNullOrEmpty(message)) throw new ArgumentNullException("message");

            Name = name;
            AttemptedValue = attemptedValue;
            MessageKey = messageKey;
            MessageLanguage = messageLanguage;
            Message = message;
            MessageValues = messageValues;
        }

        public string Name { get; private set; }
        public object AttemptedValue { get; private set; }
        public string Message { get; private set; }
        public Exception Exception { get; private set; }
        public string MessageKey { get; private set; }
        public string MessageLanguage { get; private set; }
        public object[] MessageValues { get; private set; }

        public void LocalizeMessage(Func<string, string, string> localize)
        {
            Message = localize(MessageKey, Message);

            if (MessageValues != null && MessageValues.Length > 0)
                Message = string.Format(Message, MessageValues);
        }
    }
}
