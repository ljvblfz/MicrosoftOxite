//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using Oxite.Infrastructure;
using Oxite.Modules.CMS.Models;
using Oxite.Services;
using Oxite.Validation;

namespace Oxite.Modules.CMS.Validation
{
    public class PageInputValidator : ValidatorBase<PageInput>
    {
        public PageInputValidator(ILocalizationService localizationService, IRegularExpressions expressions, OxiteContext context)
            : base(localizationService, expressions, context) { }

        #region IValidator Members

        public override ValidationState Validate(PageInput input)
        {
            if (input == null) throw new ArgumentNullException("input");

            ValidationState validationState = new ValidationState();

            string slug = input.Slug.Trim();
            if (string.IsNullOrEmpty(slug))
            {
                validationState.Errors.Add(CreateValidationError(slug, "Slug", "Slug.RequiredError", "Slug is not set."));
            }
            else
            {
                if (!Expressions.IsMatch("IsSlug", slug))
                {
                    validationState.Errors.Add(CreateValidationError(slug, "Slug", "Slug.NotValidError", "Slug is not valid."));
                }
            }

            return validationState;
        }

        #endregion
    }
}
