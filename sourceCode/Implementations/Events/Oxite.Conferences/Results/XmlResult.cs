// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using System.Web.Mvc;

namespace Oxite.Modules.Conferences.Results
{
    public class XmlResult : ViewResult
    {
        public bool IsClientCached { get; set; }

        public XmlResult(string viewName, bool isClientCached)
        {
            ViewName = viewName;
            IsClientCached = isClientCached;

            // Skin resolver wants to set this which causes errors
            MasterName = "";
        }

        public override void ExecuteResult(ControllerContext context)
        {
            TempData = context.Controller.TempData;
            ViewData = context.Controller.ViewData;

            base.ExecuteResult(context);

            if (!IsClientCached)
            {
                context.HttpContext.Response.ContentType = "text/xml";
            }
            else
            {
                context.HttpContext.Response.StatusCode = 304;
                context.HttpContext.Response.SuppressContent = true;
            }
        }
    }

}
