//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Modules.Blogs.Services;

namespace Oxite.Modules.Blogs.ModelBinders
{
    public class PostCommentModelBinder : IModelBinder
    {
        private readonly IBlogsCommentService blogsCommentService;

        public PostCommentModelBinder(IBlogsCommentService blogsCommentService)
        {
            this.blogsCommentService = blogsCommentService;
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            string blogName = (string)bindingContext.ValueProvider["blogName"].RawValue;
            string postSlug = (string)bindingContext.ValueProvider["postSlug"].RawValue;
            string commentSlug = (string)bindingContext.ValueProvider["commentSlug"].RawValue;

            return blogsCommentService.GetComment(blogName, postSlug, commentSlug);
        }
    }
}
