//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Oxite.Infrastructure;
using Oxite.Modules.Membership.Filters;
using Oxite.Modules.Membership.Models;
using Oxite.Modules.Membership.ModelBinders;
using Oxite.Modules.Membership.Repositories;
using Oxite.Modules.Membership.Repositories.SqlServer;
using Oxite.Modules.Membership.Services;
using Oxite.Modules.Membership.Validation;
using Oxite.Validation;

namespace Oxite.Modules.Membership
{
    public class MembershipModule : IOxiteModule
    {
        private readonly IUnityContainer container;

        public MembershipModule(IUnityContainer container)
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
            string[] controllerNamespaces = new string[] { "Oxite.Modules.Membership.Controllers" };

            routes.MapRoute(
                "UserFind",
                "Admin/Users",
                new { controller = "User", action = "Find", role = "Admin", validateAntiForgeryToken = true },
                null,
                controllerNamespaces
                );

            routes.MapRoute(
                "UserAdd",
                "Admin/Users/Add",
                new { controller = "User", action = "ItemAdd", role = "Admin", validateAntiForgeryToken = true },
                null,
                controllerNamespaces
                );

            routes.MapRoute(
                "UserEdit",
                "Admin/Users/{userName}/Edit",
                new { controller = "User", action = "ItemEdit", role = "Admin", validateAntiForgeryToken = true },
                null,
                controllerNamespaces
                );

            routes.MapRoute(
                "UserRemove",
                "Admin/Users/{userName}/Remove",
                new { controller = "User", action = "ItemRemove", role = "Admin", validateAntiForgeryToken = true },
                null,
                controllerNamespaces
                );

            routes.MapRoute(
                "RoleFind",
                "Admin/Roles",
                new { controller = "Role", action = "Find", role = "Admin", validateAntiForgeryToken = true },
                null,
                controllerNamespaces
                );

            routes.MapRoute(
                "RoleAdd",
                "Admin/Roles/Add",
                new { controller = "Role", action = "ItemEdit", role = "Admin", validateAntiForgeryToken = true },
                null,
                controllerNamespaces
                );

            routes.MapRoute(
                "RoleEdit",
                "Admin/Roles/{roleName}/Edit",
                new { controller = "Role", action = "ItemEdit", role = "Admin", validateAntiForgeryToken = true },
                null,
                controllerNamespaces
                );

            routes.MapRoute(
                "RoleRemove",
                "Admin/Roles/{roleName}/Remove",
                new { controller = "Role", action = "ItemRemove", role = "Admin", validateAntiForgeryToken = true },
                null,
                controllerNamespaces
                );
        }

        public void RegisterCatchAllRoutes(RouteCollection routes)
        {
        }

        public void RegisterFilters(IFilterRegistry filterRegistry)
        {
            filterRegistry.Add(Enumerable.Empty<IFilterCriteria>(), typeof(SiteAuthorizationFilter));
            filterRegistry.Add(Enumerable.Empty<IFilterCriteria>(), typeof(UserActionFilter));
        }

        public void RegisterModelBinders(ModelBinderDictionary modelBinders)
        {
            modelBinders[typeof(UserSearchCriteria)] = new UserSearchCriteriaModelBinder();
            modelBinders[typeof(UserInputAdd)] = new UserInputAddModelBinder();
            modelBinders[typeof(UserInputEdit)] = new UserInputEditModelBinder();
            modelBinders[typeof(UserAddress)] = new UserAddressModelBinder();
            modelBinders[typeof(RoleSearchCriteria)] = new RoleSearchCriteriaModelBinder();
            modelBinders[typeof(RoleInput)] = new RoleInputModelBinder();
            modelBinders[typeof(RoleAddress)] = new RoleAddressModelBinder();
        }

        public void RegisterWithContainer()
        {
            container
                .RegisterType<IUserService, UserService>()
                .RegisterType<IValidator<UserInputAdd>, UserInputAddValidator>()
                .RegisterType<IValidator<UserInputEdit>, UserInputEditValidator>()
                .RegisterType<IRoleService, RoleService>()
                .RegisterType<IValidator<RoleInput>, RoleInputValidator>();

            //TODO: (erikpo) Add a check here for if the app is running on sql or xml files
            container
                .RegisterType<OxiteMembershipDataContext>(new InjectionConstructor(new ResolvedParameter<string>("ApplicationServices")))
                .RegisterType<IUserRepository, SqlServerUserRepository>()
                .RegisterType<IRoleRepository, SqlServerRoleRepository>();
        }

        #endregion
    }
}
