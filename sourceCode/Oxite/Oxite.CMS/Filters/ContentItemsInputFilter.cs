// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using System.Linq;
using System.Web.Mvc;
using Oxite.Modules.CMS.Extensions;
using Oxite.Modules.CMS.Models;
using Oxite.Modules.CMS.ViewModels;
using Oxite.ViewModels;

namespace Oxite.Modules.CMS.Filters
{
    public class ContentItemsInputFilter : IActionFilter
    {
        #region IActionFilter Members

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            OxiteViewModel model = filterContext.Controller.ViewData.Model as OxiteViewModel;

            if (model == null)
                return;

            ContentItemsInput contentItemsInput = filterContext.HttpContext.Request.GetContentItemsInput();

            model.AddModelItem(
                contentItemsInput != null
                    ? filterContext.HttpContext.Request.GetContentItemsInput()
                    : new ContentItemsInput( 
                        model.GetModelItem<ContentItemViewModel>().ContentItems.Where(ci => ci.Page != null).Select(ci => new ContentItemInput(ci.Name, ci.DisplayName, ci.Body, ci.Published))
                        )
                    );
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        #endregion
    }
}