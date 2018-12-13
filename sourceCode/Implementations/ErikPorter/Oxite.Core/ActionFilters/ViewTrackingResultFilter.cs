//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Linq;
using Oxite.Extensions;
using Oxite.Infrastructure;
using Oxite.ViewModels;
using System.Web;
using Oxite.Models;

namespace Oxite.ActionFilters
{
    public class ViewTrackingResultFilter : IResultFilter
    {
        #region IResultFilter Members

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            //TODO: (erikpo) Check a ViewTrackingEnabled Site setting to add these or not
            object viewTrackingValue = filterContext.RouteData.Values["viewTracking"];
            bool viewTracking = viewTrackingValue != null && viewTrackingValue is bool ? (bool)viewTrackingValue : true;

            //TODO: (erikpo) some of this logic should probably move into an ActionCriteria class
            if (viewTracking && !filterContext.HttpContext.Request.ApplicationPath.TrimStart('/').StartsWith("Admin") && string.Compare(filterContext.HttpContext.Request.HttpMethod, "POST", true) != 0)
            {
                ResponseFilter responseFilter = new ResponseFilter(filterContext.HttpContext.Response.Filter);
                UrlHelper urlHelper = new UrlHelper(filterContext.RequestContext);
                string viewTrackingUrl = urlHelper.AbsolutePath(filterContext.HttpContext.Request.Url.AbsolutePath).TrimEnd('/');
                const string imgTag = "<img src=\"{0}/_View_\" width=\"1\" height=\"1\" />";

                //TODO: (erikpo) Add a site setting for whether the img should be at the beginning or the end of the inside of the body tag
                if (filterContext.RouteData.Values["dataFormat"] == null || ((string)filterContext.RouteData.Values["dataFormat"]) == "")
                    responseFilter.Inserts.Add(new ResponseInsert(string.Format(imgTag, viewTrackingUrl), ResponseInsertMode.PrependTo, "body"));
                else if (string.Compare((string)filterContext.RouteData.Values["dataFormat"], "RSS", true) == 0)
                {
                    int modelListCount = getModelListCount(filterContext.Controller.ViewData.Model);

                    for (int i = 0; i < modelListCount; i++)
                    {
                        int index = i;

                        responseFilter.Inserts.Add(new ResponseInsert(HttpUtility.HtmlEncode(string.Format(imgTag, viewTrackingUrl)), ResponseInsertMode.AppendTo, doc => findElementsInRss(doc, index), insertValue));
                    }
                }
                else if (string.Compare((string)filterContext.RouteData.Values["dataFormat"], "ATOM", true) == 0)
                {
                    int modelListCount = getModelListCount(filterContext.Controller.ViewData.Model);

                    for (int i = 0; i < modelListCount; i++)
                    {
                        int index = i;

                        responseFilter.Inserts.Add(new ResponseInsert(HttpUtility.HtmlEncode(string.Format(imgTag, viewTrackingUrl)), ResponseInsertMode.AppendTo, doc => findElementsInAtom(doc, index), insertValue));
                    }
                }

                filterContext.HttpContext.Response.Filter = responseFilter;
            }
        }

        #endregion

        //TODO: (erikpo) This method is lame, but not sure how to cast model as OxiteModelList of T
        private static int getModelListCount(object model)
        {
            OxiteModelList<Post> postListModel = model as OxiteModelList<Post>;

            if (postListModel != null) return postListModel.List.Count;

            OxiteModelList<Comment> commentListModel = model as OxiteModelList<Comment>;

            if (commentListModel != null) return commentListModel.List.Count;

            throw new ArgumentException("Unable to determine count from model");
        }

        private static IEnumerable<XElement> findElementsInRss(XDocument doc, int index)
        {
            XElement element = doc.Element("rss").Element("channel").Elements("item").ElementAt(index);
            List<XElement> elements = new List<XElement>();

            if (element != null)
                element = element.Element("description");

            if (element != null)
                elements.Add(element);

            return elements;
        }

        private static IEnumerable<XElement> findElementsInAtom(XDocument doc, int index)
        {
            XElement element = doc.Element("feed").Elements("entry").ElementAt(index);
            List<XElement> elements = new List<XElement>();

            if (element != null)
                element = element.Element("content");

            if (element != null)
                elements.Add(element);

            return elements;
        }

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
