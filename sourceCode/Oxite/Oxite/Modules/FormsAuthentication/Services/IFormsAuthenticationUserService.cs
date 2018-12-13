//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.FormsAuthentication.Models;

namespace Oxite.Modules.FormsAuthentication.Services
{
    public interface IFormsAuthenticationUserService
    {
        ModelResult ChangePassword(User user, UserChangePasswordInput userInput);
    }
}
