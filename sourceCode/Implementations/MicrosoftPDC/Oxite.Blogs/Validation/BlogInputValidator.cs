//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using Oxite.Infrastructure;
using Oxite.Modules.Blogs.Models;
using Oxite.Services;
using Oxite.Validation;

namespace Oxite.Modules.Blogs.Validation
{
    public class BlogInputValidator : ValidatorBase<BlogInput>
    {
        public BlogInputValidator(ILocalizationService localizationService, IRegularExpressions expressions, OxiteContext context)
            : base(localizationService, expressions, context) { }

        #region IValidator Members

        public override ValidationState Validate(BlogInput input)
        {
            if (input == null) throw new ArgumentNullException("input");

            ValidationState validationState = new ValidationState();

            if (string.IsNullOrEmpty(input.Name))
                validationState.Errors.Add(CreateValidationError(input.Name, "Name", "Name.RequiredError", "Name is not set."));
            else
            {
                if (!Expressions.IsMatch("BlogName", input.Name))
                    validationState.Errors.Add(CreateValidationError(input.Name, "Name", "Name.InvalidError", "Name is invalid and must be alphanumeric."));
                else if (input.Name.Length > 256)
                    validationState.Errors.Add(CreateValidationError(input.Name, "Name", "Name.MaxLengthExceededError", "Name must be less than or equal to {0} characters", 256));
            }

            if (string.IsNullOrEmpty(input.DisplayName))
                validationState.Errors.Add(CreateValidationError(input.DisplayName, "DisplayName", "DisplayName.RequiredError", "DisplayName is not set."));
            else
            {
                if (input.DisplayName.Length > 256)
                    validationState.Errors.Add(CreateValidationError(input.DisplayName, "DisplayName", "DisplayName.MaxLengthExceededError", "DisplayName must be less than or equal to {0} characters", 256));
            }

            if (!string.IsNullOrEmpty(input.Description) && input.Description.Length > 256)
                validationState.Errors.Add(CreateValidationError(input.Description, "Description", "Description.MaxLengthExceededError", "Description must be less than or equal to {0} characters", 256));

            return validationState;
        }

        #endregion
    }
}
