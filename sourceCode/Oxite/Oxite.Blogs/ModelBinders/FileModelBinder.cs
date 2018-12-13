//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Modules.Blogs.Services;

namespace Oxite.Modules.Blogs.ModelBinders
{
    public class FileModelBinder : IModelBinder
    {
        private readonly IBlogsFileService fileService;

        public FileModelBinder(IBlogsFileService fileService)
        {
            this.fileService = fileService;
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            string url = controllerContext.HttpContext.Request.Form["existingFileUrl"];

            return fileService.GetFile(url);
        }
    }
}
