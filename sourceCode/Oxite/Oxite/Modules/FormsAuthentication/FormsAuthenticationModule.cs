//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Oxite.Infrastructure;
using Oxite.Modules.FormsAuthentication.Controllers;
using Oxite.Modules.FormsAuthentication.Extensions;
using Oxite.Modules.FormsAuthentication.Models;
using Oxite.Modules.FormsAuthentication.ModelBinders;
using Oxite.Modules.FormsAuthentication.Services;
using Oxite.Modules.FormsAuthentication.Validation;
using Oxite.Modules.Membership.Filters;
using Oxite.Modules.Membership.Services;
using Oxite.Validation;

namespace Oxite.Modules.FormsAuthentication
{
    public class FormsAuthenticationModule : IOxiteAuthenticationModule
    {
        private readonly IUnityContainer container;

        public FormsAuthenticationModule(IUnityContainer container)
        {
            this.container = container;
        }

        #region IOxiteModule Members

        public void Initialize()
        {
        }

        public void Unload()
        {
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            string[] controllerNamespaces = new string[] { "Oxite.Modules.FormsAuthentication.Controllers" };

            routes.MapRoute(
                "SignIn",
                "SignIn",
                new { controller = "User", action = "SignIn" },
                null,
                controllerNamespaces
                );

            routes.MapRoute(
                "SignOut",
                "SignOut",
                new { controller = "User", action = "SignOut" },
                null,
                controllerNamespaces
                );

            routes.MapRoute(
                "UserChangePassword",
                "Admin/Users/{userName}/ChangePassword",
                new { controller = "User", action = "ChangePassword", role = "Admin", validateAntiForgeryToken = true },
                null,
                controllerNamespaces
                );
        }

        public void RegisterCatchAllRoutes(RouteCollection routes)
        {
        }

        public void RegisterFilters(IFilterRegistry filterRegistry)
        {
            ControllerActionFilterCriteria criteria = new ControllerActionFilterCriteria();

            criteria.AddMethod<UserController>(u => u.ChangePassword(null));
            criteria.AddMethod<UserController>(u => u.ChangePasswordSave(null, null));
            
            filterRegistry.Add(new[] { criteria }, typeof(AuthorizationFilter));
        }

        public void RegisterModelBinders(ModelBinderDictionary modelBinders)
        {
            modelBinders[typeof(UserChangePasswordInput)] = new UserChangePasswordInputModelBinder();
        }

        public void RegisterWithContainer()
        {
            container
                .RegisterType<IFormsAuthenticationUserService, FormsAuthenticationUserService>()
                .RegisterType<IValidator<UserChangePasswordInput>, UserChangePasswordInputValidator>()
                .RegisterType<IFormsAuthentication, FormsAuthenticationWrapper>();
        }

        #endregion

        #region IOxiteAuthenticationModule Members

        public IUser GetUser(RequestContext context)
        {
            IUserService userService = container.Resolve<IUserService>();

            if (context.HttpContext.User.Identity.IsAuthenticated)
                return userService.GetUser(context.HttpContext.User.Identity.Name);

            return new UserAnonymous();
        }

        public string GetRegistrationUrl(RequestContext context)
        {
            throw new NotImplementedException();
        }


        public string GetSignInUrl(RequestContext context)
        {
            UrlHelper urlHelper = new UrlHelper(context, container.Resolve<RouteCollection>());

            return urlHelper.SignIn(string.Compare(context.HttpContext.Request.HttpMethod, HttpVerbs.Get.ToString(), true) == 0 || context.HttpContext.Request.UrlReferrer == null
                ? context.HttpContext.Request.Url.AbsolutePath
                : context.HttpContext.Request.UrlReferrer.AbsolutePath);
        }

        public string GetSignOutUrl(RequestContext context)
        {
            UrlHelper urlHelper = new UrlHelper(context, container.Resolve<RouteCollection>());

            return urlHelper.SignOut();
        }

        #endregion
    }
}
