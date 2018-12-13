//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Results;
using Oxite.ViewModels;

namespace Oxite.Filters
{
    public abstract class FeedResultActionFilter : IActionFilter
    {
        private string feedType;

        public FeedResultActionFilter(string feedType)
        {
            this.feedType = feedType;
        }

        #region IActionFilter Members

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            object model = filterContext.Controller.ViewData.Model;

            if (model.GetType().GetGenericTypeDefinition() == typeof(OxiteModelList<>))
            {
                object list = model.GetType().GetProperty("List").GetValue(model, null);

                int count = (int)list.GetType().GetProperty("Count").GetValue(list, null);

                filterContext.Result = new FeedResult(feedType, count == 0);
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        #endregion
    }
}
