//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using Oxite.Modules.Membership.ViewModels;
using Oxite.ViewModels;

namespace Oxite.Modules.Membership.Extensions
{
    public static class OxiteViewModelExtensions
    {
        public static UserViewModel GetUser(this OxiteViewModel model)
        {
            return model.GetModelItem<UserViewModel>();
        }

        public static string GetSignInUrl(this OxiteViewModel model)
        {
            MembershipUrlViewModel viewModel = model.GetModelItem<MembershipUrlViewModel>();

            if (viewModel != null)
                if (!string.IsNullOrEmpty(viewModel.SignInUrl))
                    return viewModel.SignInUrl;

            return "";
        }

        public static string GetSignOutUrl(this OxiteViewModel model)
        {
            MembershipUrlViewModel viewModel = model.GetModelItem<MembershipUrlViewModel>();

            if (viewModel != null)
                if (!string.IsNullOrEmpty(viewModel.SignOutUrl))
                    return viewModel.SignOutUrl;

            return "";
        }

        //TODO: (erikpo) Add method for registration url
    }
}