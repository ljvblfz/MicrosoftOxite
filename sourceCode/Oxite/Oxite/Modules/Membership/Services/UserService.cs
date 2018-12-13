//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Oxite.Extensions;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Membership.Extensions;
using Oxite.Modules.Membership.Models;
using Oxite.Modules.Membership.Repositories;
using Oxite.Plugins.Extensions;
using Oxite.Plugins.Models;
using Oxite.Repositories;
using Oxite.Validation;

namespace Oxite.Modules.Membership.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository repository;
        private readonly ILanguageRepository languageRepository;
        private readonly IValidationService validator;
        private readonly IPluginEngine pluginEngine;
        private readonly IOxiteCacheModule cache;
        private readonly OxiteContext context;

        public UserService(IUserRepository repository, ILanguageRepository languageRepository, IValidationService validator, IPluginEngine pluginEngine, IModulesLoaded modules, OxiteContext context)
        {
            this.repository = repository;
            this.languageRepository = languageRepository;
            this.validator = validator;
            this.pluginEngine = pluginEngine;
            this.cache = modules.GetModules<IOxiteCacheModule>().Reverse().First();
            this.context = context;
        }

        #region IUserService Members

        public User GetUser(string name)
        {
            return cache.GetItem<User>(
                string.Format("GetUser-UserName:{0}", name),
                () => repository.GetUser(name),
                null
                );
        }

        public User GetUserByModuleData(string moduleName, string data)
        {
            return cache.GetItem<User>(
                string.Format("GetUserByModuleData-ModuleName:{0},Data:{1}", moduleName, data),
                () => repository.GetUserByModuleData(moduleName, data),
                null
                );
        }

        public IEnumerable<User> FindUsers(UserSearchCriteria criteria)
        {
            return repository.FindUsers(criteria);
        }

        public ModelResult<User> AddUser(UserInputAdd userInputAdd)
        {
            ValidationStateDictionary validationState = new ValidationStateDictionary();

            validationState.Add(typeof(UserInputAdd), validator.Validate(userInputAdd));

            if (!validationState.IsValid) return new ModelResult<User>(validationState);

            //TODO: (erikpo) Replace with some logic to set the language from the user's browser or from a dropdown list
            Language language = languageRepository.GetLanguage(context.Site.LanguageDefault ?? "en");
            User user;

            using (TransactionScope transaction = new TransactionScope())
            {
                user = userInputAdd.ToUser(e => e.ComputeHash(), EntityState.Normal, language);

                validateUser(user, validationState);

                if (!validationState.IsValid) return new ModelResult<User>(validationState);

                user = repository.Save(user);

                invalidateCachedUserDependencies(user);

                transaction.Complete();
            }

            pluginEngine.ExecuteAll("UserSaved", new { context, user = new UserReadOnly(user) });
            pluginEngine.ExecuteAll("UserAdded", new { context, user = new UserReadOnly(user) });

            return new ModelResult<User>(user, validationState);
        }

        public ModelResult<User> EditUser(User user, UserInputEdit userInputEdit)
        {
            ValidationStateDictionary validationState = new ValidationStateDictionary();

            validationState.Add(typeof(UserInputEdit), validator.Validate(userInputEdit));

            if (!validationState.IsValid) return new ModelResult<User>(validationState);

            Guid siteID = context.Site.ID;
            User originalUser = user;
            User newUser;

            using (TransactionScope transaction = new TransactionScope())
            {
                newUser = originalUser.Apply(userInputEdit, e => e.ComputeHash(), EntityState.Normal);

                validateUser(newUser, originalUser, validationState);

                if (!validationState.IsValid) return new ModelResult<User>(validationState);

                newUser = repository.Save(newUser);

                invalidateCachedUserForEdit(newUser, originalUser);

                transaction.Complete();
            }

            pluginEngine.ExecuteAll("UserSaved", new { context, user = new UserReadOnly(newUser) });
            pluginEngine.ExecuteAll("UserEdited", new { context, user = new UserReadOnly(newUser), userOriginal = new UserReadOnly(originalUser) });

            return new ModelResult<User>(newUser, validationState);
        }

        public void RemoveUser(User user)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                if (repository.Remove(user))
                {
                    invalidateCachedUserForRemove(user);

                    transaction.Complete();

                    pluginEngine.ExecuteAll("UserRemoved", new { context, user = new UserReadOnly(user) });

                    return;
                }
            }
        }

        public string GetModuleData(User user, string moduleName)
        {
            return repository.GetModuleData(user, moduleName);
        }

        public void SetModuleData(User user, string moduleName, string data)
        {
            repository.SetModuleData(user, moduleName, data);
        }

        public bool SignIn(string name)
        {
            return SignIn(() => GetUser(name), null);
        }

        public bool SignIn(Func<User> getUser, Action<User> afterSignIn)
        {
            User user = getUser();

            if (user != null)
            {
                if (afterSignIn != null)
                    afterSignIn(user);

                pluginEngine.ExecuteAll("UserSignedIn", new { context, user = new UserReadOnly(user) });

                return true;
            }

            return false;
        }

        public void SignOut()
        {
            if (context.User is User)
                pluginEngine.ExecuteAll("UserSignedOut", new { context, user = new UserReadOnly(context.User.Cast<User>()) });
        }

        public void EnsureAnonymousUser()
        {
            User user = GetUser("Anonymous");

            if (user == null)
            {
                //TODO: (erikpo) Replace with some logic to set the language from the user's browser or from a dropdown list
                Language language = languageRepository.GetLanguage(context.Site.LanguageDefault ?? "en");

                user = new User("Anonymous", "", "", "", language, EntityState.Normal);

                repository.Save(user);
            }
        }

        #endregion

        #region Private Methods

        private void invalidateCachedUserDependencies(User user)
        {
            // UserAuthenticated doesn't have any dependencies
        }

        private void invalidateCachedUserForEdit(User newUser, User originalUser)
        {
            // UserAuthenticated doesn't have any dependencies

            cache.InvalidateItem(newUser);
        }

        private void invalidateCachedUserForRemove(User user)
        {
            invalidateCachedUserDependencies(user);

            cache.InvalidateItem(user);
        }

        private void validateUser(User newUser, ValidationStateDictionary validationState)
        {
            validateUser(newUser, newUser, validationState);
        }

        private void validateUser(User newUser, User originalUser, ValidationStateDictionary validationState)
        {
            ValidationState state = new ValidationState();
            User foundUser;

            validationState.Add(typeof(User), state);

            foundUser = repository.GetUser(newUser.Name);

            if (foundUser != null && newUser.Name != originalUser.Name)
                state.Errors.Add(new ValidationError("User.NameNotUnique", newUser.Name, "A user already exists with the supplied name"));
        }

        #endregion
    }
}
