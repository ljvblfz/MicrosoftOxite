//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Specialized;
using System.Web.Mvc;
using Oxite.Modules.Blogs.Models;

namespace Oxite.Modules.Blogs.ModelBinders
{
    public class BlogInputModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            NameValueCollection form = controllerContext.HttpContext.Request.Form;
            string name = form.Get("blogName");
            string displayName = form.Get("blogDisplayName");
            string description = form.Get("blogDescription");

            bool commentingEnabled;
            bool.TryParse(form["blogCommentingEnabled"] != null ? form.GetValues("blogCommentingEnabled")[0] : "false", out commentingEnabled);

            return new BlogInput(name, displayName, description, !commentingEnabled);
        }
    }
}