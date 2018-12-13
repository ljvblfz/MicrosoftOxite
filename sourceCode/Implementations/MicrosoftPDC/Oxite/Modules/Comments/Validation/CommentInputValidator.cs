//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using Oxite.Infrastructure;
using Oxite.Modules.Comments.Models;
using Oxite.Services;
using Oxite.Validation;

namespace Oxite.Modules.Comments.Validation
{
    public class CommentInputValidator : ValidatorBase<CommentInput>
    {
        public CommentInputValidator(ILocalizationService localizationService, IRegularExpressions expressions, OxiteContext context)
            : base(localizationService, expressions, context) { }

        #region IValidator Members

        public override ValidationState Validate(CommentInput input)
        {
            if (input == null) throw new ArgumentNullException("input");

            ValidationState validationState = new ValidationState();

            if (string.IsNullOrEmpty(input.Body))
                validationState.Errors.Add(CreateValidationError(input.Body, "Body", "Body.RequiredError", "Comment body is not set."));

            if (input.Creator != null)
            {
                if (string.IsNullOrEmpty(input.Creator.Name))
                    validationState.Errors.Add(CreateValidationError(input.Creator.Name, "Creator.Name", "Creator.Name.RequiredError", "Comment creator name is not set."));

                if (input.Subscribe && string.IsNullOrEmpty(input.Creator.Email))
                    validationState.Errors.Add(CreateValidationError(input.Creator.Email, "Creator.Email", "Creator.Subscribe.EmailRequiredError", "Comment creator email must be set to subscribe"));

                if (!string.IsNullOrEmpty(input.Creator.Email) && !Expressions.IsMatch("IsEmail", input.Creator.Email))
                    validationState.Errors.Add(CreateValidationError(input.Creator.Email, "Creator.Email", "Creator.EmailInvalid", "Comment creator email is not valid."));

                if (!string.IsNullOrEmpty(input.Creator.Url) && !Expressions.IsMatch("IsUrl", input.Creator.Url))
                    validationState.Errors.Add(CreateValidationError(input.Creator.Url, "Creator.Url", "Creator.UrlInvalid", "Comment creator url is not valid."));
            }

            return validationState;
        }

        #endregion
    }
}
