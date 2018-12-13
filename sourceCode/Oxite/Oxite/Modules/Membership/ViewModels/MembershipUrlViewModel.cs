//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
namespace Oxite.Modules.Membership.ViewModels
{
    public class MembershipUrlViewModel
    {
        public MembershipUrlViewModel(string signInUrl, string signOutUrl)
        {
            SignInUrl = signInUrl;
            SignOutUrl = signOutUrl;
        }

        public string SignInUrl { get; set; }
        public string SignOutUrl { get; set; }
        //TODO: (erikpo) Add RegistrationUrl
    }
}