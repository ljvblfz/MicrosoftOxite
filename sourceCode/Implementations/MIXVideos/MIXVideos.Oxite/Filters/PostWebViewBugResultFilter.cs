//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using MIXVideos.Oxite.Extensions;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.ViewModels;

namespace MIXVideos.Oxite.Filters
{
    public class PostWebViewBugResultFilter : IResultFilter
    {
        #region IResultFilter Members

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            OxiteModelItem<Post> model = filterContext.Controller.ViewData.Model as OxiteModelItem<Post>;

            if (model != null)
            {
                UrlHelper urlHelper = new UrlHelper(filterContext.RequestContext);
                string imgTag = string.Format("<div style=\"position:absolute;left:-9999px;\"><img src=\"{0}\" width=\"1\" height=\"1\" alt=\"Web View Bug\" /></div>", urlHelper.PostViewBug(model.Item, "Post-Web"));
                ResponseFilter responseFilter = new ResponseFilter(filterContext.HttpContext.Response.Filter, filterContext.HttpContext);

                responseFilter.Inserts.Add(new ResponseInsert(imgTag, ResponseInsertMode.PrependTo, "body"));

                filterContext.HttpContext.Response.Filter = responseFilter;
            }
        }

        #endregion
    }
}
