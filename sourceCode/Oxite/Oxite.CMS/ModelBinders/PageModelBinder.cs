//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Modules.CMS.Models;
using Oxite.Modules.CMS.Services;

namespace Oxite.Modules.CMS.ModelBinders
{
    public class PageModelBinder : IModelBinder
    {
        private readonly IPageService pageService;

        public PageModelBinder(IPageService pageService)
        {
            this.pageService = pageService;
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            string pagePath = (string)bindingContext.ValueProvider["pagePath"].RawValue;
            string actionName = bindingContext.ValueProvider.ContainsKey("action") ? bindingContext.ValueProvider["action"].RawValue as string : null;

            if (actionName == "ItemAdd")
                pagePath = pagePath.Substring(0, pagePath.Length - (1 + PageMode.Add.ToString().Length));
            if (actionName == "ItemEdit")
                pagePath = pagePath.Substring(0, pagePath.Length - (1 + PageMode.Edit.ToString().Length));
            if (actionName == "ItemAddContent")
                pagePath = pagePath.Substring(0, pagePath.Length - (1 + PageMode.AddContent.ToString().Length));
            if (actionName == "ItemEditContent")
                pagePath = pagePath.Substring(0, pagePath.Length - (1 + PageMode.EditContent.ToString().Length));
            if (actionName == "Remove")
                pagePath = pagePath.Substring(0, pagePath.Length - (1 + PageMode.Remove.ToString().Length));

            return pageService.GetPage(pagePath);
        }
    }
}
