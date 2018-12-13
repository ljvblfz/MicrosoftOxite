//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Membership.Extensions;
using Oxite.Modules.Membership.Models;
using Oxite.Modules.Membership.Repositories;
using Oxite.Plugins.Extensions;
using Oxite.Plugins.Models;
using Oxite.Services;
using Oxite.Validation;

namespace Oxite.Modules.Membership.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository repository;
        private readonly IValidationService validator;
        private readonly IPluginEngine pluginEngine;
        private readonly OxiteContext context;

        public RoleService(IRoleRepository repository, IValidationService validator, IPluginEngine pluginEngine, OxiteContext context)
        {
            this.repository = repository;
            this.validator = validator;
            this.pluginEngine = pluginEngine;
            this.context = context;
        }

        #region IRoleService Members

        public Role GetRole(string name)
        {
            return repository.GetRole(name);
        }

        public IEnumerable<Role> GetSiteRoles()
        {
            return repository.GetSiteRoles().ToArray();
        }

        public IEnumerable<Role> FindRoles(RoleSearchCriteria criteria)
        {
            return repository.FindRoles(criteria).ToArray();
        }

        public ModelResult<Role> AddRole(RoleInput roleInput)
        {
            ValidationStateDictionary validationState = new ValidationStateDictionary();

            validationState.Add(typeof(RoleInput), validator.Validate(roleInput));

            if (!validationState.IsValid) return new ModelResult<Role>(validationState);

            Role role;

            using (TransactionScope transaction = new TransactionScope())
            {
                role = roleInput.ToRole();

                validateRole(role, validationState);

                if (!validationState.IsValid) return new ModelResult<Role>(validationState);

                role = repository.Save(role);

                transaction.Complete();
            }

            pluginEngine.ExecuteAll("RoleSaved", new { context, role = new RoleReadOnly(role) });
            pluginEngine.ExecuteAll("RoleAdded", new { context, role = new RoleReadOnly(role) });

            return new ModelResult<Role>(role, validationState);
        }

        public ModelResult<Role> EditRole(Role role, RoleInput roleInput)
        {
            ValidationStateDictionary validationState = new ValidationStateDictionary();

            validationState.Add(typeof(RoleInput), validator.Validate(roleInput));

            if (!validationState.IsValid) return new ModelResult<Role>(validationState);

            Role originalRole = role;
            Role newRole;

            using (TransactionScope transaction = new TransactionScope())
            {
                newRole = originalRole.Apply(roleInput);

                validateRole(newRole, originalRole, validationState);

                if (!validationState.IsValid) return new ModelResult<Role>(validationState);

                newRole = repository.Save(newRole);

                transaction.Complete();
            }

            pluginEngine.ExecuteAll("RoleSaved", new { context, role = new RoleReadOnly(newRole) });
            pluginEngine.ExecuteAll("RoleEdited", new { context, role = new RoleReadOnly(newRole), roleOriginal = new RoleReadOnly(originalRole) });

            return new ModelResult<Role>(newRole, validationState);
        }

        public void RemoveRole(Role role)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                if (repository.Remove(role))
                {
                    transaction.Complete();

                    pluginEngine.ExecuteAll("RoleRemoved", new { context, role = new RoleReadOnly(role) });

                    return;
                }
            }
        }

        #endregion

        #region Private Methods

        private void validateRole(Role newRole, ValidationStateDictionary validationState)
        {
            validateRole(newRole, newRole, validationState);
        }

        private void validateRole(Role newRole, Role originalRole, ValidationStateDictionary validationState)
        {
            ValidationState state = new ValidationState();
            Role foundRole;

            validationState.Add(typeof(Role), state);

            foundRole = repository.GetRole(newRole.Name);

            if (foundRole != null && newRole.Name != originalRole.Name)
                state.Errors.Add("Role.NameNotUnique", newRole.Name, "A role already exists with the supplied name");
        }

        #endregion
    }
}
