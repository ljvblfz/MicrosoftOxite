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
    public class PageAddressModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            string pagePath = controllerContext.RouteData.GetRequiredString("pagePath");

            string actionName = controllerContext.RouteData.Values["action"] as string;

            if (actionName == "ItemAdd")
                pagePath = pagePath.Substring(0, pagePath.Length - (1 + PageMode.Add.ToString().Length));
            if (actionName == "ItemEdit")
                pagePath = pagePath.Substring(0, pagePath.Length - (1 + PageMode.Edit.ToString().Length));
            if (actionName == "ItemEditContent")
                pagePath = pagePath.Substring(0, pagePath.Length - (1 + PageMode.EditContent.ToString().Length));
            if (actionName == "Remove")
                pagePath = pagePath.Substring(0, pagePath.Length - (1 + PageMode.Remove.ToString().Length));

            pagePath = pagePath.TrimEnd('/');

            return new PageAddress(pagePath);
        }
    }
}
