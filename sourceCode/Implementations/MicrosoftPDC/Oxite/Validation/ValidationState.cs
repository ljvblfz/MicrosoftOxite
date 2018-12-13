//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;

namespace Oxite.Validation
{
    public class ValidationState
    {
        public ValidationState()
        {
            Errors = new ValidationErrorCollection();
        }

        public ValidationState(IEnumerable<ValidationError> errors)
            : this()
        {
            if (errors != null)
                foreach (ValidationError error in errors)
                    Errors.Add(error);
        }

        public ValidationErrorCollection Errors { get; private set; }

        public bool IsValid
        {
            get
            {
                return Errors.Count == 0;
            }
        }
    }
}
