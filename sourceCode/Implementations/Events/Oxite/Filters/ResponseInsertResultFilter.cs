// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using System.Web.Mvc;
using Oxite.Infrastructure;

namespace Oxite.Filters
{
    public class ResponseInsertResultFilter : IResultFilter
    {
        #region IResultFilter Members

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (filterContext.Result is ViewResult)
            {
                ResponseFilter responseFilter = new ResponseFilter(filterContext.HttpContext.Response.Filter, filterContext.HttpContext);

                //responseFilter.Inserts.Add(
                //    new ResponseInsert("<div>right after the h1</div>",
                //        ResponseInsertMode.InsertAfter, "h1"));
                //responseFilter.Inserts.Add(
                //    new ResponseInsert("<div>something before the end of the body</div>",
                //        ResponseInsertMode.AppendTo, "body"));
                //responseFilter.Inserts.Add(
                //    new ResponseInsert("<div>something after the beginning of the body</div>",
                //        ResponseInsertMode.PrependTo, "body"));
                //responseFilter.Inserts.Add(
                //    new ResponseInsert("<h4>some content after the #page div</h4><div>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean quis metus ac dui bibendum fermentum. Vestibulum id nulla non diam scelerisque eleifend. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Nullam nec justo et eros accumsan vestibulum. Sed et nisi libero. Nulla vel cursus sapien. In lorem magna, venenatis eu iaculis a, viverra ut sapien. Donec fringilla lacus ac neque fermentum dapibus. Nulla id auctor erat. Phasellus in ullamcorper mauris. Morbi sed convallis magna. Aenean consectetur arcu eget tellus imperdiet malesuada. Morbi dignissim convallis velit. Sed turpis tellus, sodales luctus volutpat id, feugiat ut nisi. Nunc malesuada egestas neque, quis molestie odio interdum a.</div>",
                //        ResponseInsertMode.InsertAfter, "div#page"));
                //responseFilter.Inserts.Add(
                //    new ResponseInsert("<div class=\"sub\">something before the archives</div>",
                //        ResponseInsertMode.InsertBefore, ".archives"));
                //responseFilter.Inserts.Add(
                //    new ResponseInsert("<a href=\"#\">replaced archive link</a>",
                //        ResponseInsertMode.ReplaceWith, ".archives li a"));
                //responseFilter.Inserts.Add(
                //    new ResponseInsert(null,
                //        ResponseInsertMode.Remove, "ul.posts .more"));
                //responseFilter.Inserts.Add(
                //    new ResponseInsert("<span class=\"awesome\">{0}</span>",
                //        ResponseInsertMode.Wrap, "input,textarea"));

                filterContext.HttpContext.Response.Filter = responseFilter;
            }
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
        }

        #endregion
    }
}
