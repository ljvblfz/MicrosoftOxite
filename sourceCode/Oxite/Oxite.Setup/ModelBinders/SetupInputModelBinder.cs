//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------

namespace Oxite.Modules.Setup.ModelBinders
{
    using System;
    using System.Collections.Specialized;
    using System.Web.Mvc;
    using Oxite.Modules.Setup.Models;

    /// <summary>
    /// Class to enable model binding for the SetupInput object.
    /// </summary>
    public class SetupInputModelBinder : IModelBinder
    {
        /// <summary>
        /// Method called when form data needs to be bound to a SetupInput instance.
        /// </summary>
        /// <param name="controllerContext">ControllerContext containing the form data to be bound.</param>
        /// <param name="bindingContext">ModelBindingContext</param>
        /// <returns>SetupInput instance bound to the data in the ControllerContext.</returns>
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            NameValueCollection form = controllerContext.HttpContext.Request.Form;

            Guid siteId = Guid.Empty;

            string contextSiteId = controllerContext.RouteData.Values["siteId"].ToString();

            if (form.Get("siteId") != null)
            {
                siteId = new Guid(form.Get("siteId"));
            }
            else if (!string.IsNullOrEmpty(contextSiteId))
            {
                siteId = new Guid(contextSiteId);
            }

            string siteDisplayName = form.Get("siteDisplayName");
            string siteDescription = form.Get("siteDescription");

            string adminUserName = form["adminUserName"];
            string adminDisplayName = form["adminDisplayName"];
            string adminEmail = form["adminEmail"];
            string adminPassword = form["adminPassword"];
            string adminPasswordConfirm = form["adminPasswordConfirm"];
            string siteType = form["siteType"];
            string storageType = form["storageType"];

            bool commentingEnabled;
            bool.TryParse(form["blogCommentingEnabled"] != null ? form.GetValues("blogCommentingEnabled")[0] : "false", out commentingEnabled);

            SetupInput input = new SetupInput()
            {
                SiteID = siteId,
                AdminUserName = adminUserName,
                AdminDisplayName = adminDisplayName,
                AdminEmail = adminEmail,
                AdminPassword = adminPassword,
                AdminPasswordConfirm = adminPasswordConfirm,
                SiteDescription = siteDescription,
                SiteDisplayName = siteDisplayName
            };


            if (!string.IsNullOrEmpty(siteType))
            {
                input.SiteType = (SiteType)Enum.Parse(typeof(SiteType), siteType);
            }

            if (!string.IsNullOrEmpty(storageType))
            {
                input.StorageType = (StorageType)Enum.Parse(typeof(StorageType), storageType);
            }

            return input;
        }
    }
}
