//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oxite.Extensions;
using Oxite.Modules.CMS.Extensions;
using Oxite.Modules.CMS.Models;

namespace Oxite.Modules.CMS.ModelBinders
{
    public class PageInputModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            HttpRequestBase request = controllerContext.HttpContext.Request;

            if (string.Compare(request.HttpMethod, HttpVerbs.Post.ToString(), true) == 0)
            {
                DateTime? published = null;
                if (request.Form.IsTrue("isPublished"))
                {
                    DateTime publishedValue;

                    published = DateTime.TryParse(request.Form.Get("page_published"), out publishedValue) ? publishedValue : (DateTime?)null;
                }

                //TODO: (erikpo) Fill in template name once its in the form
                string templateName = ""; //request.Form.Get("TemplateName");
                //TODO: (erikpo) Fill in template name once its in the form
                string title = ""; //request.Form.Get("page_title");
                //TODO: (erikpo) Fill in template name once its in the form
                string description = ""; //request.Form.Get("page_description");
                string slug = request.Form.Get("page_slug");

                return new PageInput(templateName, title, description, slug, published);
            }

            return null;
        }
    }
}
