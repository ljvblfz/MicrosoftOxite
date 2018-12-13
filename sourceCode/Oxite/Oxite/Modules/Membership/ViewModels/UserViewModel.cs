//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using Oxite.Infrastructure;

namespace Oxite.Modules.Membership.ViewModels
{
    public class UserViewModel
    {
        private readonly UserAnonymous userAnonymous;
        private readonly User user;
        private readonly UserUnregistered userUnregistered;

        public UserViewModel(IUser user)
        {
            Name = user.Name;
            IsAuthenticated = user.IsAuthenticated;
            AuthenticationValues = user.AuthenticationValues;

            if (user is UserAnonymous)
            {
                userAnonymous = (UserAnonymous)user;

                DisplayName = user.Name;
                Email = userAnonymous.Email;
                EmailHash = userAnonymous.EmailHash;
                Url = userAnonymous.Url;
            }
            else if (user is User)
            {
                this.user = (User)user;

                DisplayName = this.user.DisplayName;
                Email = this.user.Email;
                EmailHash = this.user.EmailHash;
                Url = "";
            }
            else if (user is UserUnregistered)
            {
                userUnregistered = (UserUnregistered)user;

                DisplayName = "";
                Email = "";
                EmailHash = "";
                Url = "";
            }
        }

        public string Name { get; private set; }
        public string DisplayName { get; private set; }
        public string Email { get; private set; }
        public string EmailHash { get; private set; }
        public string Url { get; private set; }
        public bool IsAuthenticated { get; private set; }
        public IDictionary<string, object> AuthenticationValues { get; private set; }

        public bool IsInRole(string roleName)
        {
            return user != null && user.IsInRole(roleName);
        }

        //public bool IsInRole(string roleName, Blog blog)
        //{
        //    if (user == null) return false;

        //    return user.IsInRole(roleName, blog);
        //}

        //public bool IsInRole(string roleName, Post post)
        //{
        //    if (user == null) return false;

        //    return user.IsInRole(roleName, post);
        //}

        //public bool IsInRole(string roleName, Page page)
        //{
        //    if (user == null) return false;

        //    return user.IsInRole(roleName, page);
        //}

        public UserAnonymous ToUserAnonymous()
        {
            return userAnonymous;
        }

        public User ToUserAuthenticated()
        {
            return user;
        }

        public UserUnregistered ToUserUnregistered()
        {
            return userUnregistered;
        }
    }
}
