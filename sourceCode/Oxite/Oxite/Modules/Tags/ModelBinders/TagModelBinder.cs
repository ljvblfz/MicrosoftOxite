//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Modules.Tags.Services;

namespace Oxite.Modules.Tags.ModelBinders
{
    public class TagModelBinder : IModelBinder
    {
        private readonly ITagService tagService;

        public TagModelBinder(ITagService tagService)
        {
            this.tagService = tagService;
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            string tagName = (string)bindingContext.ValueProvider["tagName"].RawValue;

            return tagService.GetTag(tagName);
        }
    }
}
