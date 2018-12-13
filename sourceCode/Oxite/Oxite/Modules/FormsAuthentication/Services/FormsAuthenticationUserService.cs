//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.FormsAuthentication.Extensions;
using Oxite.Modules.FormsAuthentication.Models;
using Oxite.Modules.Membership.Repositories;
using Oxite.Plugins.Extensions;
using Oxite.Validation;

namespace Oxite.Modules.FormsAuthentication.Services
{
    public class FormsAuthenticationUserService : IFormsAuthenticationUserService
    {
        private readonly IUserRepository repository;
        private readonly IValidationService validator;
        private readonly IPluginEngine pluginEngine;
        private readonly OxiteContext context;

        public FormsAuthenticationUserService(IUserRepository repository, IValidationService validator, IPluginEngine pluginEngine, OxiteContext context)
        {
            this.repository = repository;
            this.validator = validator;
            this.pluginEngine = pluginEngine;
            this.context = context;
        }

        #region IFormsAuthenticationUserService Members

        public ModelResult ChangePassword(User user, UserChangePasswordInput userInput)
        {
            ValidationStateDictionary validationState = new ValidationStateDictionary();

            validationState.Add(typeof(UserChangePasswordInput), validator.Validate(userInput));

            if (!validationState.IsValid) return new ModelResult(validationState);

            string passwordSalt = Guid.NewGuid().ToString("N");
            string password = userInput.Password.SaltAndHash(passwordSalt);

            repository.SetModuleData(user, "FormsAuthentication", string.Format("{0}|{1}", passwordSalt, password));

            pluginEngine.ExecuteAll("UserChangePassword", new { user, context });

            return new ModelResult();
        }

        #endregion
    }
}
