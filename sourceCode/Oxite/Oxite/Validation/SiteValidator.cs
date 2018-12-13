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
    public class SiteValidator : ValidatorBase<Site>
    {
        public SiteValidator(ILocalizationService localizationService, IRegularExpressions expressions, OxiteContext context)
            : base(localizationService, expressions, context) { }

        #region IValidator Members

        public override ValidationState Validate(Site input)
        {
            if (input == null) throw new ArgumentNullException("input");

            ValidationState validationState = new ValidationState();

            if (string.IsNullOrEmpty(input.CommentStateDefault))
            {
                validationState.Errors.Add(CreateValidationError(input.CommentStateDefault, "CommentStateDefault", "CommentStateDefault.RequiredError", "CommentStateDefault is not set."));
            }
            else
            {
                if (!(input.CommentStateDefault == EntityState.Normal.ToString() || input.CommentStateDefault == EntityState.PendingApproval.ToString()))
                {
                    validationState.Errors.Add(CreateValidationError(input.CommentStateDefault, "CommentStateDefault", "CommentStateDefault.InvalidError", "Invalid value specified for CommentStateDefault."));
                }
            }

            if (string.IsNullOrEmpty(input.SkinsStylesPath))
            {
                validationState.Errors.Add(CreateValidationError(input.SkinsStylesPath, "SkinsStylesPath", "CssPath.RequiredError", "CssPath is not set."));
            }

            if (string.IsNullOrEmpty(input.DisplayName))
            {
                validationState.Errors.Add(CreateValidationError(input.DisplayName, "DisplayName", "DisplayName.RequiredError", "DisplayName is not set."));
            }

            if (input.Host == null)
            {
                validationState.Errors.Add(CreateValidationError(input.Host, "Host", "Host.RequiredError", "Host is not set."));
            }

            if (string.IsNullOrEmpty(input.LanguageDefault))
            {
                validationState.Errors.Add(CreateValidationError(input.LanguageDefault, "LanguageDefault", "LanguageDefault.RequiredError", "LanguageDefault is not set."));
            }

            if (string.IsNullOrEmpty(input.Name))
            {
                validationState.Errors.Add(CreateValidationError(input.Name, "Name", "Name.RequiredError", "Name is not set."));
            }

            if (string.IsNullOrEmpty(input.PageTitleSeparator))
            {
                validationState.Errors.Add(CreateValidationError(input.PageTitleSeparator, "PageTitleSeparator", "PageTitleSeparator.RequiredError", "PageTitleSeparator is not set."));
            }

            if (string.IsNullOrEmpty(input.SkinsScriptsPath))
            {
                validationState.Errors.Add(CreateValidationError(input.SkinsScriptsPath, "SkinsScriptsPath", "ScriptsPath.RequiredError", "ScriptsPath is not set."));
            }

            if (string.IsNullOrEmpty(input.AdminSkin))
            {
                validationState.Errors.Add(CreateValidationError(input.AdminSkin, "AdminSkin", "AdminSkin.RequiredError", "AdminSkin is not set."));
            }

            return validationState;
        }

        #endregion
    }
}
