//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------

using System.Web.Mvc;
using Oxite.Modules.Conferences.Results;
using Oxite.ViewModels;

namespace Oxite.Modules.Conferences.Filters
{
    public class IcsResultFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.Result = new IcsResult("ItemIcs", false);
        }
    }

    public class IcsResultActionFilter : IActionFilter
    {
        #region IActionFilter Members

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            object model = filterContext.Controller.ViewData.Model;

            if (model.GetType().GetGenericTypeDefinition() == typeof(OxiteViewModelItems<>))
            {
                object list = model.GetType().GetProperty("Items").GetValue(model, null);

                int count = (int)list.GetType().GetProperty("Count").GetValue(list, null);

                filterContext.Result = new IcsResult("ICS", count == 0);
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        #endregion

    }
}