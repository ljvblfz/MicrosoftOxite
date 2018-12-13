//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using Oxite.Models;
using Oxite.Modules.FormsAuthentication.Models;
using Oxite.Modules.Membership.Models;
using Oxite.Services;

namespace Oxite.Modules.FormsAuthentication.Services
{
    public interface IFormsAuthenticationUserService
    {
        ModelResult ChangePassword(UserAddress userAddress, UserChangePasswordInput userInput);
    }
}
