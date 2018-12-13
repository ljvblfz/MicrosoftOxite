//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
namespace Oxite.Modules.Setup.Services
{
    using System;
    using Oxite.Infrastructure;
    using Oxite.Models;
    using Oxite.Modules.Blogs.Models;
    using Oxite.Modules.Blogs.Services;
    using Oxite.Modules.Membership.Models;
    using Oxite.Modules.Membership.Repositories;
    using Oxite.Modules.Membership.Services;
    using Oxite.Services;
    using Oxite.Validation;
    using Oxite.Modules.Setup.Models;
    using System.Transactions;

    public class SetupService : ISetupService
    {
        #region Fields
        private readonly ISiteService siteService;
        private readonly IUserService userService;
        private readonly IUserRepository repository;
        private readonly IValidationService validator;
        private readonly IPluginEngine pluginEngine;
        private readonly ILanguageService languageService;
        #endregion

        #region Constructor
        public SetupService(ISiteService siteService, IUserService userService, IUserRepository repository, IValidationService validator, IPluginEngine pluginEngine, ILanguageService languageService)
        {
            this.siteService = siteService;
            this.userService = userService;
            this.repository = repository;
            this.validator = validator;
            this.pluginEngine = pluginEngine;
            this.languageService = languageService;
        }
        #endregion

        public Site SetupSite(SetupInput input, out ValidationStateDictionary validationState)
        {
            Site site = null;

            using (TransactionScope transaction = new TransactionScope())
            {
                site = this.CreateSite(out validationState);

                if (!validationState.IsValid)
                {
                    return null;
                }

                UserInputAdd userInput = new UserInputAdd(input.AdminUserName, input.AdminDisplayName, input.AdminEmail, input.AdminPassword, input.AdminPasswordConfirm);

                ModelResult<User> results = this.SetupUser(userInput);

                if (!results.IsValid)
                {
                    validationState = results.ValidationState;

                    return null;
                }

                transaction.Complete();
            }

            return site;
        }

        private Site CreateSite(out ValidationStateDictionary validationState)
        {
            Site site = this.siteService.GetSite();

            if (site != null)
            {
                //throw new InvalidOperationException("A site has already been set up.  Please contact the administrator to make changes.");
            }

            Language language = new Language { Name = "en", DisplayName = "English" };
            languageService.AddLanguage(language);

            Site siteInput = new Site
            {
                Name = "Oxite Site",
                DisplayName = "My Oxite Site",
                Description = "An Oxite based web site.",
                FavIconUrl = "/Content/icons/flame.ico",
                LanguageDefault = "en",
                PageTitleSeparator = "-",
                GravatarDefault = "http://mschnlnine.vo.llnwd.net/d1/oxite/gravatar.jpg",
                SkinsPath = "/Skins",
                SkinsScriptsPath = "/Scripts",
                SkinsStylesPath = "/Styles",
                Skin = "Default",
                AdminSkin = "Admin",
                RouteUrlPrefix = string.Empty,
                PluginsPath = "/Plugins",
                CommentStateDefault = "PendingApproval",
                Host = new Uri("http://localhost:30913"),
                TimeZoneOffset = -8,
                PostEditTimeout = 24,
                IncludeOpenSearch = true,
                AuthorAutoSubscribe = true
            };

            siteService.AddSite(siteInput, out validationState, out site);

            userService.EnsureAnonymousUser();

            return site;
        }

        private ModelResult<User> SetupUser(UserInputAdd userInput)
        {
            return this.userService.AddUser(userInput);
        }

    }
}
