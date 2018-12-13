//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using Oxite.Models;
using OxiteSite.App_Code.Modules.OxiteSite.Models;
using OxiteSite.App_Code.Modules.OxiteSite.Repositories;

namespace OxiteSite.App_Code.Modules.OxiteSite.Services
{
    public class PDC09Service : IPDC09Service
    {
        private readonly IRegistrationRepository repository;

        public PDC09Service(IRegistrationRepository repository)
        {
            this.repository = repository;
        }

        #region IPDC09Service Members

        public UserRegistration GetUserRegistration(UserAuthenticated user)
        {
            return repository.GetUserRegistration(user.ID);
        }

        public void SetUserRegistration(UserAuthenticated user, bool isRegistered)
        {
            repository.SetUserRegistration(user.ID, isRegistered);
        }

        public List<Guid> GetUserSessions(UserAuthenticated user)
        {
            return repository.GetUserSessions(user.ID);
        }

        #endregion
    }
}
