//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using Oxite.Models;
using Oxite.Infrastructure;

namespace Oxite.ViewModels
{
    public class UserViewModel
    {
        private readonly IUser user;
        private readonly UserAnonymous userAnonymous;
        private readonly UserAuthenticated userAuthenticated;
        private readonly UserUnregistered userUnregistered;

        public UserViewModel(IUser user)
        {
            this.user = user;

            if (user is UserAnonymous)
            {
                userAnonymous = (UserAnonymous)user;

                DisplayName = user.Name;
                Email = userAnonymous.Email;
                EmailHash = userAnonymous.EmailHash;
                Url = userAnonymous.Url;
            }
            else if (user is UserAuthenticated)
            {
                userAuthenticated = (UserAuthenticated)user;

                DisplayName = userAuthenticated.DisplayName;
                Email = userAuthenticated.Email;
                EmailHash = userAuthenticated.EmailHash;
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

        public string Name { get { return user.Name; } }
        public string DisplayName { get; private set; }
        public string Email { get; private set; }
        public string EmailHash { get; private set; }
        public string Url { get; private set; }
        public bool IsAuthenticated { get { return user.IsAuthenticated; } }
        public IDictionary<string, object> AuthenticationValues { get { return user.AuthenticationValues; } }

        public bool IsInRole(string roleName)
        {
            return user.IsInRole(roleName);
        }

        //public bool IsInRole(string roleName, Blog blog)
        //{
        //    if (userAuthenticated == null) return false;

        //    return userAuthenticated.IsInRole(roleName, blog);
        //}

        //public bool IsInRole(string roleName, Post post)
        //{
        //    if (userAuthenticated == null) return false;

        //    return userAuthenticated.IsInRole(roleName, post);
        //}

        //public bool IsInRole(string roleName, Page page)
        //{
        //    if (userAuthenticated == null) return false;

        //    return userAuthenticated.IsInRole(roleName, page);
        //}

        public IUser ToUser()
        {
            return user;
        }

        public UserAnonymous ToUserAnonymous()
        {
            return userAnonymous;
        }

        public UserAuthenticated ToUserAuthenticated()
        {
            return userAuthenticated;
        }

        public UserUnregistered ToUserUnregistered()
        {
            return userUnregistered;
        }
    }
}
