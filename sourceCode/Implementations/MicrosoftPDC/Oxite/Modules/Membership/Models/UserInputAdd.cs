//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using Oxite.Models;

namespace Oxite.Modules.Membership.Models
{
    public class UserInputAdd : UserInput
    {
        public UserInputAdd(string userName, string displayName, string email, string password, string passwordConfirm)
            : base(userName, displayName, email)
        {
            Password = password;
            PasswordConfirm = passwordConfirm;
        }

        public string Password { get; private set; }
        public string PasswordConfirm { get; private set; }

        public UserAuthenticated ToUser(Func<string, string> computeEmailHash, EntityState status)
        {
            string passwordSalt = Guid.NewGuid().ToString("N");

            return new UserAuthenticated(UserName, DisplayName, Email, computeEmailHash(Email), status);
        }

        public UserAuthenticated ToUser(Func<string, string> computeEmailHash, EntityState status, Language language)
        {
            string passwordSalt = Guid.NewGuid().ToString("N");

            return new UserAuthenticated(UserName, DisplayName, Email, computeEmailHash(Email), language, status);
        }
    }
}
