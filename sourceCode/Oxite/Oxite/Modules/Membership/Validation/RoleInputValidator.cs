//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Membership.Models;
using Oxite.Services;
using Oxite.Validation;

namespace Oxite.Modules.Membership.Validation
{
    public class RoleInputValidator : ValidatorBase<RoleInput>
    {
        public RoleInputValidator(ILocalizationService localizationService, IRegularExpressions expressions, OxiteContext context)
            : base(localizationService, expressions, context) { }

        #region IValidator Members

        public override ValidationState Validate(RoleInput input)
        {
            if (input == null) throw new ArgumentNullException("input");

            ValidationState validationState = new ValidationState();

            if (string.IsNullOrEmpty(input.RoleName))
                validationState.Errors.Add(CreateValidationError(input.RoleName, "RoleName", "RoleName.RequiredError", "Role Name is not set"));
            else
            {
                if (input.RoleName.Length > 256)
                    validationState.Errors.Add(CreateValidationError(input.RoleName, "RoleName", "RoleName.MaxLengthExceededError", "Role Name must be less than or equal to {0} characters long.", 256));
            }

            if (input.RoleType == RoleType.NotSet)
                validationState.Errors.Add(CreateValidationError(input.RoleType, "RoleType", "RoleType.RequiredError", "One or more role types must be set"));

            return validationState;
        }

        #endregion
    }
}
