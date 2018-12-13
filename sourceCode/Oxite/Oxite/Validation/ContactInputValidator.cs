//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Services;

namespace Oxite.Validation
{
    public class ContactInputValidator : ValidatorBase<ContactInput>
    {
        public ContactInputValidator(ILocalizationService localizationService, IRegularExpressions expressions, OxiteContext context)
            : base(localizationService, expressions, context) { }

        #region IValidator Members

        public override ValidationState Validate(ContactInput input)
        {
            if (input == null) throw new ArgumentNullException("input");

            ValidationState validationState = new ValidationState();

            if (string.IsNullOrEmpty(input.Message))
                validationState.Errors.Add(CreateValidationError(input.Message, "Message", "Message.RequiredError", "Message is not set."));

            if (input.Email != null)
            {
                if (string.IsNullOrEmpty(input.Email))
                    validationState.Errors.Add(CreateValidationError(input.Email, "Email", "ContactForm.Email", "Email must be set to submit feedback"));

                if (!string.IsNullOrEmpty(input.Email) && !Expressions.IsMatch("IsEmail", input.Email))
                    validationState.Errors.Add(CreateValidationError(input.Email, "Email", "UserBase.Email", "Email is not valid."));
            }

            return validationState;
        }

        #endregion

    }
}
