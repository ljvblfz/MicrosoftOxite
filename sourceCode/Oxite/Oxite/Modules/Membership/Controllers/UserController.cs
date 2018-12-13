//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Oxite.Extensions;
using Oxite.Infrastructure;
using Oxite.Models;
//HACK: (erikpo) This is part of the hack below and should be removed
using Oxite.Modules.FormsAuthentication.Extensions;
using Oxite.Modules.Membership.Models;
using Oxite.Modules.Membership.Services;
using Oxite.ViewModels;

namespace Oxite.Modules.Membership.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IRoleService roleService;

        public UserController(IUserService userService, IRoleService roleService)
        {
            this.userService = userService;
            this.roleService = roleService;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public OxiteViewModelItems<User> Find()
        {
            //TODO: (erikpo) Check permissions

            return new OxiteViewModelItems<User>();
        }

        [ActionName("Find"), AcceptVerbs(HttpVerbs.Post)]
        public OxiteViewModelItems<User> FindQuery(UserSearchCriteria searchCriteria)
        {
            //TODO: (erikpo) Check permissions

            IEnumerable<User> foundUsers = userService.FindUsers(searchCriteria);
            //TODO: (erikpo) Before the list is set into the model, filter it from the AuthorizationManager class (like make sure anonymous doesn't ever get sent down, etc)
            OxiteViewModelItems<User> model = new OxiteViewModelItems<User>(foundUsers);

            model.AddModelItem(searchCriteria);

            return model;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public OxiteViewModelItem<User> ItemAdd()
        {
            //TODO: (erikpo) Check permissions

            return new OxiteViewModelItem<User>(null);
        }

        [ActionName("ItemAdd"), AcceptVerbs(HttpVerbs.Post)]
        public object ItemSaveAdd(UserInputAdd userInput)
        {
            //TODO: (erikpo) Check permissions

            ModelResult<User> results = userService.AddUser(userInput);

            if (!results.IsValid)
            {
                ModelState.AddModelErrors(results.ValidationState);

                return ItemAdd();
            }

            //HACK: (erikpo) This should be moved out into some event saying "ok, i'm done adding the user, module, do you want to do something else too?" (maybe in the AddUser service call instead)
            string passwordSalt = Guid.NewGuid().ToString("N");
            string password = userInput.Password.SaltAndHash(passwordSalt);
            userService.SetModuleData(results.Item, "FormsAuthentication", string.Format("{0}|{1}", passwordSalt, password));

            return Redirect(Url.AppPath(Url.ManageUsers()));
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public OxiteViewModelItem<User> ItemEdit(User user)
        {
            if (user == null) return null;

            //TODO: (erikpo) Check permissions

            return new OxiteViewModelItem<User>(user);
        }

        [ActionName("ItemEdit"), AcceptVerbs(HttpVerbs.Post)]
        public object ItemSaveEdit(User user, UserInputEdit userInput)
        {
            //TODO: (erikpo) Check permissions

            ModelResult<User> results = userService.EditUser(user, userInput);

            if (!results.IsValid)
            {
                ModelState.AddModelErrors(results.ValidationState);

                return ItemEdit(user);
            }

            return Redirect(Url.AppPath(Url.ManageUsers()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public object ItemRemove(User user)
        {
            if (user == null) return null;

            //TODO: (erikpo) Check permissions

            userService.RemoveUser(user);

            return Redirect(Url.AppPath(Url.ManageUsers()));
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public OxiteViewModelItemItems<User, Role> Roles(User user)
        {
            if (user == null) return null;

            //TODO: (erikpo) Check permissions

            IEnumerable<Role> roles = roleService.GetSiteRoles();

            return new OxiteViewModelItemItems<User, Role>(user, roles);
        }
    }
}
