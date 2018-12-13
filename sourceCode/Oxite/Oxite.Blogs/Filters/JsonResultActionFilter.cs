//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using System.Web.Mvc;
using Oxite.Extensions;
using Oxite.Models;
using Oxite.Modules.Blogs.Models;
using Oxite.ViewModels;

namespace Oxite.Modules.Blogs.Filters
{
    public class JsonResultActionFilter : IActionFilter
    {
        #region IActionFilter Members

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            object model = filterContext.Controller.ViewData.Model;

            //TODO: (erikpo) Need to figure out a more "generic" way to do this

            if (model.GetType().GetGenericTypeDefinition() == typeof(OxiteViewModelItems<>))
            {
                object list = model.GetType().GetProperty("Items").GetValue(model, null);
                string json = null;

                if (list is IPageOfItems<Post>)
                    json = ((List<PostComment>)list).ToArray().ToJson();
                else if (list is IPageOfItems<PostComment>)
                    json = ((List<PostComment>)list).ToArray().ToJson();

                filterContext.Result = new ContentResult() { Content = json, ContentType = "application/json" };
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        #endregion
    }
}
