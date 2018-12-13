//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using MIXVideos.Oxite.Extensions;
using Oxite.Extensions;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.ViewModels;

namespace MIXVideos.Oxite.Filters
{
    public abstract class PostFeedViewBugResultFilter : IResultFilter
    {
        private string viewType;
        private Func<XDocument, int, IEnumerable<XElement>> findElements;

        public PostFeedViewBugResultFilter(string viewType, Func<XDocument, int, IEnumerable<XElement>> findElements)
        {
            this.viewType = viewType;
            this.findElements = findElements;
        }

        #region IResultFilter Members

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            OxiteModelList<Post> model = filterContext.Controller.ViewData.Model as OxiteModelList<Post>;

            if (model != null)
            {
                UrlHelper urlHelper = new UrlHelper(filterContext.RequestContext);
                ResponseFilter responseFilter = new ResponseFilter(filterContext.HttpContext.Response.Filter, filterContext.HttpContext);

                for (int i = 0; i < model.List.Count; i++)
                {
                    Post post = model.List[i];
                    string imgTag = HttpUtility.HtmlEncode(string.Format("<div><img src=\"{0}\" width=\"1\" height=\"1\" alt=\"\" /></div>", urlHelper.AbsolutePath(urlHelper.PostViewBug(post, viewType))));

                    int index = i;

                    responseFilter.Inserts.Add(new ResponseInsert(imgTag, ResponseInsertMode.AppendTo, doc => findElements(doc, index), (elements, mode, value) => insertValue(elements, mode, value)));
                }

                filterContext.HttpContext.Response.Filter = responseFilter;
            }
        }

        #endregion

        private static void insertValue(IEnumerable<XElement> elements, ResponseInsertMode mode, string value)
        {
            switch (mode)
            {
                case ResponseInsertMode.AppendTo:
                    insertValueOnElements(elements, e => e.Value += value);
                    break;
                case ResponseInsertMode.PrependTo:
                    insertValueOnElements(elements, e => e.Value = value + e.Value);
                    break;
                case ResponseInsertMode.ReplaceWith:
                case ResponseInsertMode.InsertBefore:
                case ResponseInsertMode.InsertAfter:
                    throw new NotSupportedException();
            }
        }

        private static void insertValueOnElements(IEnumerable<XElement> elements, Action<XElement> method)
        {
            foreach (XElement element in elements)
                method(element);
        }
    }
}
