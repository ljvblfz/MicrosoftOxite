//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Membership.Models;

namespace Oxite.Modules.Membership.Extensions
{
    public static class UserExtensions
    {
        public static User Apply(this User user, UserInputEdit input, Func<string, string> computeEmailHash, EntityState status)
        {
            return new User(user.ID, input.UserName, input.DisplayName, input.Email, computeEmailHash(input.Email), user.LanguageDefault, status);
        }
    }
}
