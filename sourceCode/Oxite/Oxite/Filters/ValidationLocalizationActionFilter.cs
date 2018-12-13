//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Oxite.Extensions;
using Oxite.Validation;
using Oxite.ViewModels;

namespace Oxite.Filters
{
    public class ValidationLocalizationActionFilter : IActionFilter
    {
        #region IActionFilter Members

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            OxiteViewModel model = filterContext.Controller.ViewData.Model as OxiteViewModel;
            ValidationStateDictionary validationState = filterContext.Controller.ViewData["ValidationState"] as ValidationStateDictionary;

            if (model != null && validationState != null)
                foreach (KeyValuePair<Type, ValidationState> validationStateItem in validationState)
                    foreach (ValidationError error in validationStateItem.Value.Errors)
                        if (!string.IsNullOrEmpty(error.MessageKey))
                            error.LocalizeMessage((key, defaultValue) => model.Localize(key, defaultValue));

            if (validationState != null)
                filterContext.Controller.ViewData.ModelState.AddModelErrors(validationState);
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        #endregion
    }
}
