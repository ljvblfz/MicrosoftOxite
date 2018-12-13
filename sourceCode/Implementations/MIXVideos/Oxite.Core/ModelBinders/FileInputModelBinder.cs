//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web;
using System.Web.Mvc;
using Oxite.Models;

namespace Oxite.ModelsBinders
{
    public class FileInputModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            HttpRequestBase request = controllerContext.HttpContext.Request;
            int contentLength = 0;

            int.TryParse(request.Form["fileSizeInBytes"], out contentLength);

            return new FileInput(request.Form["fileTypeName"], request.Form["fileUrl"], request.Form["fileMimeType"], contentLength);
        }
    }
}
