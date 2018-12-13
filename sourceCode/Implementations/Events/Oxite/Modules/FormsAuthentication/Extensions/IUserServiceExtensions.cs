//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using Oxite.Models;
using Oxite.Modules.Membership.Services;

namespace Oxite.Modules.FormsAuthentication.Extensions
{
    public static class IUserServiceExtensions
    {
        public static UserAuthenticated GetUser(this IUserService userService, string name, string password)
        {
            UserAuthenticated user = userService.GetUser(name);

            if (user != null)
            {
                string data = userService.GetModuleData(user.ID, "FormsAuthentication");

                if (data != null)
                {
                    string[] splitData = data.Split('|');

                    if (splitData.Length != 2) throw new InvalidOperationException("Password and PasswordSalt could not be read from module data");

                    string userPasswordSalt = splitData[0];
                    string userPassword = splitData[1];

                    if (userPassword == password.SaltAndHash(userPasswordSalt))
                        return user;
                }
            }

            return null;
        }
    }
}
