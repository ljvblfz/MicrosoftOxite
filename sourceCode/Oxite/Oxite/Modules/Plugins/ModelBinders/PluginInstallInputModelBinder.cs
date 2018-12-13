//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Modules.Plugins.Models;

namespace Oxite.Modules.Plugins.ModelBinders
{
    public class PluginInstallInputModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            return new PluginInstallInput(controllerContext.HttpContext.Request.Form["virtualPath"]);
        }
    }
}
