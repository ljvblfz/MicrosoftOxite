//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using Oxite.Infrastructure;
using Oxite.Modules.FormsAuthentication.Models;
using Oxite.Services;
using Oxite.Validation;

namespace Oxite.Modules.FormsAuthentication.Validation
{
    public class UserChangePasswordInputValidator : ValidatorBase<UserChangePasswordInput>
    {
        public UserChangePasswordInputValidator(ILocalizationService localizationService, IRegularExpressions expressions, OxiteContext context)
            : base(localizationService, expressions, context) { }

        #region IValidator Members

        public override ValidationState Validate(UserChangePasswordInput input)
        {
            if (input == null) throw new ArgumentNullException("input");

            ValidationState validationState = new ValidationState();

            if (string.IsNullOrEmpty(input.Password))
            {
                validationState.Errors.Add(CreateValidationError(input.Password, "Password", "Password.RequiredError", "Password is not set"));
            }
            if (string.IsNullOrEmpty(input.PasswordConfirm))
            {
                validationState.Errors.Add(CreateValidationError(input.PasswordConfirm, "PasswordConfirm", "PasswordConfirm.RequiredError", "Password (Confirm) is not set"));
            }
            if (!string.IsNullOrEmpty(input.Password) && !string.IsNullOrEmpty(input.PasswordConfirm) && input.Password != input.PasswordConfirm)
            {
                validationState.Errors.Add(CreateValidationError(input.PasswordConfirm, "PasswordConfirm", "PasswordConfirm.NoMatchWithPassword", "Passwords do not match"));
            }

            return validationState;
        }

        #endregion
    }
}
