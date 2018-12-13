//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using Oxite.Infrastructure;
using Oxite.Modules.Membership.Models;
using Oxite.Services;
using Oxite.Validation;

namespace Oxite.Modules.Membership.Validation
{
    public class UserInputAddValidator : ValidatorBase<UserInputAdd>
    {
        public UserInputAddValidator(ILocalizationService localizationService, IRegularExpressions expressions, OxiteContext context)
            : base(localizationService, expressions, context) { }

        #region IValidator Members

        public override ValidationState Validate(UserInputAdd input)
        {
            if (input == null) throw new ArgumentNullException("input");

            ValidationState validationState = new ValidationState();

            if (string.IsNullOrEmpty(input.UserName))
                validationState.Errors.Add(CreateValidationError(input.UserName, "UserName", "UserName.RequiredError", "Username is not set"));
            else
            {
                if (input.UserName.Length > 256)
                    validationState.Errors.Add(CreateValidationError(input.UserName, "UserName", "UserName.MaxLengthExceededError", "Username must be less than or equal to {0} characters long.", 256));
            }

            if (string.IsNullOrEmpty(input.DisplayName))
                validationState.Errors.Add(CreateValidationError(input.DisplayName, "DisplayName", "DisplayName.RequiredError", "DisplayName is not set"));
            else
            {
                if (input.DisplayName.Length > 256)
                    validationState.Errors.Add(CreateValidationError(input.DisplayName, "DisplayName", "DisplayName.MaxLengthExceededError", "DisplayName must be less than or equal to {0} characters long.", 256));
            }

            if (!string.IsNullOrEmpty(input.Email))
            {
                if (input.Email.Length > 256)
                    validationState.Errors.Add(CreateValidationError(input.Email, "Email", "Email.MaxLengthExceededError", "Email must be less than or equal to {0} characters long.", 256));
                else if (!Expressions.IsMatch("IsEmail", input.Email))
                    validationState.Errors.Add(CreateValidationError(input.Email, "Email", "Email.InvalidError", "Email is invalid."));
            }

            //if (string.IsNullOrEmpty(input.Password))
            //    validationState.Errors.Add(CreateValidationError(input.Password, "Password", "Password.RequiredError", "Password is not set"));
            //if (string.IsNullOrEmpty(input.PasswordConfirm))
            //    validationState.Errors.Add(CreateValidationError(input.PasswordConfirm, "PasswordConfirm", "PasswordConfirm.RequiredError", "Password (Confirm) is not set"));
            //if (!string.IsNullOrEmpty(input.Password) && !string.IsNullOrEmpty(input.PasswordConfirm) && input.Password != input.PasswordConfirm)
            //    validationState.Errors.Add(CreateValidationError(input.PasswordConfirm, "PasswordConfirm", "PasswordConfirm.NoMatchWithPassword", "Passwords do not match"));

            return validationState;
        }

        #endregion
    }
}
