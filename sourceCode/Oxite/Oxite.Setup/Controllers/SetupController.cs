//---------------------------------------------------------------------
// <copyright file="SetupController.cs" company="Microsoft">
//      This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//      http://www.codeplex.com/oxite/license
// </copyright>
//---------------------------------------------------------------------
using Oxite.Configuration;
using Oxite.Infrastructure;

namespace Oxite.Modules.Setup.Controllers
{
    using System;
    using System.Web.Mvc;
    using Oxite.Extensions;
    using Oxite.Models;
    using Oxite.Modules.Blogs.Models;
    using Oxite.Modules.Membership.Models;
    using Oxite.Modules.Setup.Models;
    using Oxite.Modules.Setup.Services;
    using Oxite.Services;
    using Oxite.Validation;
    using Oxite.ViewModels;

    /// <summary>
    /// Controller to manage actions related to Oxite site setup
    /// </summary>
    public class SetupController : Controller
    {
        #region Fields
        /// <summary>
        /// Setup service to use to setup basic information about the site.
        /// </summary>
        private readonly ISetupService setupService;

        /// <summary>
        /// Site service to retrieve details of the site.
        /// </summary>
        private readonly ISiteService siteService;

        /// <summary>
        /// Oxite configuration with details about available modules.
        /// </summary>
        private readonly OxiteConfigurationSection config;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the SetupController class.
        /// </summary>
        /// <param name="setupService">Setup service to use to setup basic information about the site.</param>
        /// <param name="siteService">Site service to retrieve details of the site.</param>
        /// <param name="config">Oxite config with retrieve details about available modules.</param>
        public SetupController(ISetupService setupService, ISiteService siteService, OxiteConfigurationSection config)
        {
            this.setupService = setupService;
            this.siteService = siteService;
            this.config = config;
        }
        #endregion

        /// <summary>
        /// Display the view that allows the user to update the Admin account settings for
        /// the new site.
        /// </summary>
        /// <param name="input">SetupInput instance containing all user entered information about the site and its
        /// components.</param>
        /// <returns>OxiteViewModelItem with its Item property set to the current SetupInput instance.</returns>
        [ActionName("AdminSettings")]
        [AcceptVerbs(HttpVerbs.Post)]
        public virtual object AdminSettingsSave(SetupInput input)
        {
            ViewData["CurrentWizardPage"] = 2;

            return new OxiteViewModelItem<SetupInput>(input);
        }

        /// <summary>
        /// Display the view that allows the user to update some basic settings for
        /// the new site.
        /// </summary>
        /// <param name="input">SetupInput instance containing all user entered information about the site and its
        /// components.</param>
        /// <returns>OxiteViewModelItem with its Item property set to the current SetupInput instance.</returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public virtual object BasicSettings(SetupInput input)
        {
            ViewData["CurrentWizardPage"] = 3;

            return new OxiteViewModelItem<SetupInput>(input);
        }

        /// <summary>
        /// Display the view that allows the user to select the modules to use in the new site.
        /// </summary>
        /// <param name="input">SetupInput instance containing all user entered information about the site and its
        /// components.</param>
        /// <returns>OxiteViewModelItem with its Item property set to the current SetupInput instance.</returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public virtual object Modules(SetupInput input)
        {
            input.InitializeModules(config);

            ViewData["CurrentWizardPage"] = 4;

            return new OxiteViewModelItem<SetupInput>(input);
        }

        /// <summary>
        /// Create site based on user input.  Includes creating site entry, Admin account, anonymous user account,
        /// and adding selected modules.  If setup is successful, navigate to success page.  Otherwise, return to page with
        /// error messaging.
        /// </summary>
        /// <param name="input">SetupInput instance containing all user entered information about the site and its
        /// components.</param>
        /// <returns>OxiteViewModelItem that has its Item property set to the new Site.</returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public virtual object SetupComplete(SetupInput input)
        {
            ValidationStateDictionary validationState;

            Site site = this.setupService.SetupSite(input, out validationState);

            if (!validationState.IsValid)
            {
                ModelState.AddModelErrors(validationState);

                return this.Modules(input);
            }

            ViewData["CurrentWizardPage"] = 5;

            return new OxiteViewModelItem<Site>(site);
        }

        /// <summary>
        /// Display the view that allows the user to select the type of site they would like to set up.  This will
        /// default standard modules and determine the call to action links on the setup complete page.
        /// </summary>
        /// <param name="input">SetupInput instance containing all user entered information about the site and its
        /// components.</param>
        /// <returns>OxiteViewModelItem with its Item property set to the current SetupInput instance.</returns>
        [ActionName("SiteType")]
        [AcceptVerbs(HttpVerbs.Get)]
        public virtual object SiteType(SetupInput input)
        {
            Site site = this.siteService.GetSite();

            if (site != null)
            {
                // throw new InvalidOperationException("A site has already been set up.  Please contact the administrator to make changes.");
            }

            ViewData["CurrentWizardPage"] = 0;

            return new OxiteViewModelItem<SetupInput>(input);
        }

        /// <summary>
        /// Display the view that allows the user to select the way that post/page/etc data
        /// will be stored for the new site.
        /// </summary>
        /// <param name="input">SetupInput instance containing all user entered information about the site and its
        /// components.</param>
        /// <returns>Redirects to the screen to setup admin settings if successful.  Storage view otherwise.</returns>
        [ActionName("Storage")]
        [AcceptVerbs(HttpVerbs.Post)]
        public virtual object StorageSave(SetupInput input)
        {
            ViewData["CurrentWizardPage"] = 1;

            return new OxiteViewModelItem<SetupInput>(input);
        }
    }
}
