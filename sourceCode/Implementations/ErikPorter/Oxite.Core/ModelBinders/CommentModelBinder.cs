//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Web;
using System.Web.Mvc;
using Oxite.Extensions;
using Oxite.Models;

namespace Oxite.ModelsBinders
{
    public class CommentModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            HttpRequestBase request = controllerContext.HttpContext.Request;

            Comment comment = new Comment
            {
                Body = request.Form.Get("body"),
                CreatorIP = request.GetUserIPAddress().ToLong(),
                CreatorUserAgent = request.UserAgent
            };

            if (!string.IsNullOrEmpty(request.Form.Get("id")))
                comment.ID = new Guid(request.Form.Get("id"));

            return comment;
        }
    }
}
