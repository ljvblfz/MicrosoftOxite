//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;
using Oxite.Extensions;
using Oxite.Modules.Plugins.Models;

namespace Oxite.Modules.Plugins.ModelBinders
{
    public class PluginEditInputModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            HttpRequestBase request = controllerContext.HttpContext.Request;

            if (string.Compare(request.HttpMethod, HttpVerbs.Post.ToString(), true) == 0 && request.Form.Count > 0)
            {
                string virtualPath = request.Form["virtualPath"];
                string code = request.Form["code"];
                NameValueCollection properties = null;
                const string propertyFormPrefix = "ps_";

                if (string.IsNullOrEmpty(code))
                {
                    properties = new NameValueCollection(10);

                    foreach (string formKey in request.Form.Keys)
                        if (formKey.StartsWith(propertyFormPrefix))
                            properties.Add(formKey.Substring(propertyFormPrefix.Length), request.Form.GetValues(formKey)[0]);
                }

                //TODO: (nheskew)need to handle different input types, differently. what would multi-value params look like (if supported)

                return new PluginEditInput(virtualPath, code, request.Form.IsTrueNullable("enabled"), properties);
            }

            return null;
        }
    }
}
