//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Oxite.Extensions;
using Oxite.Modules.Blogs.Models;

namespace Oxite.Modules.Blogs.ModelBinders
{
    public class PostInputModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            HttpRequestBase request = controllerContext.HttpContext.Request;

            if (string.Compare(request.HttpMethod, HttpVerbs.Post.ToString(), true) == 0 && request.Form.Count > 0)
            {
                string blogName = request.Form["blogName"];
                string title = request.Form["title"];
                string body = request.Form["body"];
                string bodyShort = request.Form["bodyShort"];
                string slug = request.Form["slug"];
                bool commentingDisabled = !request.Form.IsTrue("commentingEnabled");

                DateTime? published = null;
                if (request.Form.IsTrue("isPublished"))
                {
                    DateTime publishedValue;

                    published = DateTime.TryParse(request.Form["published"], out publishedValue) ? publishedValue : DateTime.UtcNow;
                }

                List<string> tags = new List<string>();
                if (!string.IsNullOrEmpty(request.Form["tags"]))
                    foreach (string tagName in request.Form["tags"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                        if (tagName.Trim().Length > 0)
                            tags.Add(tagName.Trim());

                return new PostInput(blogName, title, body, bodyShort, tags, slug, published, commentingDisabled);
            }

            return null;
        }
    }
}
