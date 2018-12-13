//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using Oxite.Validation;

namespace Oxite.Models
{
    public class ModelResult
    {
        public ModelResult()
            : this(null)
        {
        }

        public ModelResult(ValidationStateDictionary validationState)
        {
            ValidationState = validationState;
            IsValid = validationState == null || validationState.IsValid;
        }

        public ValidationStateDictionary ValidationState { get; private set; }
        public bool IsValid { get; private set; }

        public Exception GetFirstException()
        {
            if (!IsValid)
                foreach (KeyValuePair<Type, ValidationState> item in ValidationState)
                    if (item.Value.Errors.Count > 0)
                        return item.Value.Errors[0].Exception;

            return null;
        }
    }
}
