//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Web.Mvc;
using Oxite.Modules.CMS.Models;

namespace Oxite.Modules.CMS.ModelBinders
{
    public class ContentItemInputModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            string publishedDateStringValue = controllerContext.HttpContext.Request.Form.Get("publishedDate");
            DateTime? publishedDate = null;

            if (!string.IsNullOrEmpty(publishedDateStringValue))
            {
                DateTime publishedDateValue;
                if (DateTime.TryParse(publishedDateStringValue, out publishedDateValue))
                    publishedDate = publishedDateValue;
            }

            return new ContentItemInput(
                controllerContext.HttpContext.Request.Form.Get("name"),
                controllerContext.HttpContext.Request.Form.Get("displayName"),
                controllerContext.HttpContext.Request.Form.Get("body"),
                publishedDate
                );
        }
    }
}
