//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Services;
using Oxite.ViewModels;

namespace Oxite.Filters
{
    public class LocalizationActionFilter : IActionFilter, IExceptionFilter
    {
        private readonly ILocalizationService locService;

        public LocalizationActionFilter(ILocalizationService locService)
        {
            this.locService = locService;
        }

        #region IActionFilter Members

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ViewResult result = filterContext.Result as ViewResult;

            if (result != null)
                setLocalization(result.ViewData.Model as OxiteModel);
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        #endregion

        #region IExceptionFilter Members

        public void OnException(ExceptionContext filterContext)
        {
            setLocalization(filterContext.Controller.ViewData.Model as OxiteModel);
        }

        #endregion

        private void setLocalization(OxiteModel model)
        {
            if (model != null)
                model.AddModelItem(locService.GetTranslations());
        }
    }
}
