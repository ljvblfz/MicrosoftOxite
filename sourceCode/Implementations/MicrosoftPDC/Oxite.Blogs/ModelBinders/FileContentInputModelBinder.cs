//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web;
using System.Web.Mvc;
using Oxite.Modules.Blogs.Models;

namespace Oxite.Modules.Blogs.ModelBinders
{
    public class FileContentInputModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            HttpRequestBase request = controllerContext.HttpContext.Request;
            byte[] content = new byte[request.Files[0].ContentLength];

            request.Files[0].InputStream.Read(content, 0, request.Files[0].ContentLength);

            return new FileContentInput(request.Form["fileTypeName"], request.Files[0].ContentType, content);
        }
    }
}
