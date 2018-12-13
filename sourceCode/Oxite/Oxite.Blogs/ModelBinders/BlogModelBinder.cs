//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Modules.Blogs.Services;

namespace Oxite.Modules.Blogs.ModelBinders
{
    public class BlogModelBinder : IModelBinder
    {
        private readonly IBlogService blogService;

        public BlogModelBinder(IBlogService blogService)
        {
            this.blogService = blogService;
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            string blogName = bindingContext.ValueProvider.ContainsKey("blogName") ? bindingContext.ValueProvider["blogName"].AttemptedValue : null;

            return !string.IsNullOrEmpty(blogName) ? blogService.GetBlog(blogName) : null;
        }
    }
}
