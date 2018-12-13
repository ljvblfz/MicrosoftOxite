//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.ViewModels;

namespace Oxite.Filters
{
    public class SiteActionFilter : IActionFilter, IExceptionFilter
    {
        private readonly AppSettingsHelper appSettings;
        private readonly Site site;

        public SiteActionFilter(AppSettingsHelper appSettings, Site site)
        {
            this.appSettings = appSettings;
            this.site = site;
        }

        #region IActionFilter Members

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            setModel(filterContext.Controller.ViewData.Model as OxiteModel);
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        #endregion

        #region IExceptionFilter Members

        public void OnException(ExceptionContext filterContext)
        {
            setModel(filterContext.Controller.ViewData.Model as OxiteModel);
        }

        #endregion

        private void setModel(OxiteModel model)
        {
            if (model != null)
            {
                model.Site = new SiteViewModel(site, appSettings.GetString("SiteName", "Oxite"));
            }
        }
    }
}
