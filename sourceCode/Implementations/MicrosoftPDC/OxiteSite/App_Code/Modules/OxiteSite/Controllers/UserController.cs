// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using System.Web.Mvc;
using Oxite.Extensions;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Membership.Models;
using Oxite.Modules.Membership.Services;
using Oxite.ViewModels;

namespace OxiteSite.App_Code.Modules.OxiteSite.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly OxiteContext context;

        public UserController(IUserService userService, OxiteContext context)
        {
            this.userService = userService;
            this.context = context;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public OxiteViewModel Register()
        {
            return new OxiteViewModel();
        }

        [AcceptVerbs(HttpVerbs.Post), ActionName("Register")]
        public object RegisterSave(string username, string displayName)
        {
            ModelResult<UserAuthenticated> result = userService.AddUser(new UserInputAdd(username, displayName, "", "", ""));

            if (!result.IsValid)
            {
                ModelState.AddModelErrors(result.ValidationState);

                return Register();
            }

            userService.SetModuleData(result.Item.Name, "LiveID", (string)context.User.AuthenticationValues["PUID"]);

            return Redirect(Url.Home());
        }
    }
}
