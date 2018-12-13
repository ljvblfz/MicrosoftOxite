//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Oxite.Extensions;
using Oxite.Infrastructure;
using Oxite.Services;
using Oxite.ViewModels;

namespace Oxite.Filters
{
    public class SiteActionFilter : IActionFilter, IExceptionFilter
    {
        private readonly OxiteContext context;
        private readonly OxiteConfigurationSection config;

        public SiteActionFilter(OxiteContext context, OxiteConfigurationSection config)
        {
            this.context = context;
            this.config = config;
        }

        #region IActionFilter Members

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            setModel(filterContext.Controller.ViewData.Model as OxiteViewModel);
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        #endregion

        #region IExceptionFilter Members

        public void OnException(ExceptionContext filterContext)
        {
            setModel(filterContext.Controller.ViewData.Model as OxiteViewModel);
        }

        #endregion

        private void setModel(OxiteViewModel model)
        {
            if (model != null)
                model.Site = new SiteViewModel(context.Site, config.InstanceName);
        }
    }
}
