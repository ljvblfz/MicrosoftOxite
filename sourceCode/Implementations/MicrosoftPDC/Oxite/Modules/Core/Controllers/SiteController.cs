//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Oxite.Extensions;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Membership.Models;
using Oxite.Modules.Membership.Services;
using Oxite.Services;
using Oxite.Validation;
using Oxite.ViewModels;

namespace Oxite.Modules.Core.Controllers
{
    public class SiteController : Controller
    {
        private readonly string oxiteInstanceName;
        private readonly ISiteService siteService;
        private readonly IUserService userService;
        //private readonly IBlogService blogService;
        private readonly ILanguageService languageService;

        public SiteController(OxiteConfigurationSection config, ISiteService siteService, IUserService userService/*, IBlogService blogService*/, ILanguageService languageService)
        {
            this.oxiteInstanceName = config.InstanceName;
            this.siteService = siteService;
            this.userService = userService;
            //this.blogService = blogService;
            this.languageService = languageService;
        }

        public virtual OxiteViewModel Dashboard()
        {
            return new OxiteViewModel { Container = new AdminDashboardPageContainer() };
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public virtual OxiteViewModelItem<Site> Item()
        {
            Site site = siteService.GetSite()
                ?? new Site
                {
                    Name = oxiteInstanceName,
                    DisplayName = "My Oxite Site",
                    Description = "",
                    PageTitleSeparator = " - ",
                    TimeZoneOffset = -8,
                    CommentStateDefault = EntityState.PendingApproval.ToString(),
                    AuthorAutoSubscribe = true,
                    RouteUrlPrefix = "",
                    IncludeOpenSearch = true,
                    LanguageDefault = "en",
                    GravatarDefault = "http://mschnlnine.vo.llnwd.net/d1/oxite/gravatar.jpg",
                    PostEditTimeout = 24,
                    SkinsPath = "/Skins",
                    SkinsScriptsPath = "/Scripts",
                    SkinsStylesPath = "/Styles",
                    Skin = "Default",
                    AdminSkin = "Admin",
                    FavIconUrl = "/Content/icons/flame.ico",
                    HasMultipleBlogs = false,
                    CommentingDisabled = false,
                    PluginsPath = "/Plugins"
                };

            return new OxiteViewModelItem<Site>(site);
        }

        [ActionName("Item"), AcceptVerbs(HttpVerbs.Post)]
        public virtual object SaveItem(Site siteInput, UserInputAdd userInput)
        {
            ValidationStateDictionary validationState;

            if (siteInput.ID == Guid.Empty && userInput != null)
            {
                //TODO: (erikpo) This is lame.  Add a SiteInput class and a SiteInputValidator class and move the following validation code into it.
                IUnityContainer container = (IUnityContainer)HttpContext.Application["container"];
                IValidator<Site> siteValidator = container.Resolve<IValidator<Site>>();

                validationState = new ValidationStateDictionary();

                validationState.Add(typeof(Site), siteValidator.Validate(siteInput));

                if (validationState.IsValid)
                {
                    Site site;

                    siteService.AddSite(siteInput, out validationState, out site);

                    Language language = new Language { Name = "en", DisplayName = "English" };
                    languageService.AddLanguage(language);

                    userService.EnsureAnonymousUser();

                    //UserAuthenticated user;

                    //userInput.Status = (byte)EntityState.Normal;
                    //userInput.LanguageDefault = language;

                    //userService.AddUser(userInput, out validationState, out user);

                    if (validationState.IsValid)
                    {
                        //Blog blog = new Blog
                        //{
                        //    CommentingDisabled = false,
                        //    Name = "Blog",
                        //    DisplayName = site.DisplayName,
                        //    Description = site.Description
                        //};

                        //blogService.AddBlog(blog, site, out validationState, out blog);
                    }
                }
            }
            else
            {
                siteService.EditSite(siteInput, out validationState);
            }

            if (!validationState.IsValid)
            {
                ModelState.AddModelErrors(validationState);

                return Item();
            }

            OxiteApplication.Load(ControllerContext.HttpContext);

            return Redirect(Url.AppPath(Url.ManageSite()));
        }
    }
}
