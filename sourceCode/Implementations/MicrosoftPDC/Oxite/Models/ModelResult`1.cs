//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using Oxite.Validation;

namespace Oxite.Models
{
    public class ModelResult<T> : ModelResult
    {
        public ModelResult(ValidationStateDictionary validationState)
            : base(validationState)
        {
        }

        public ModelResult(T result, ValidationStateDictionary validationState)
            : base(validationState)
        {
            Item = result;
        }

        public T Item { get; private set; }
    }
}
