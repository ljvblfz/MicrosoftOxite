//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Web;
using System.Web.Mvc;
using Oxite.Extensions;
using Oxite.Infrastructure;
using Oxite.Modules.Comments.Models;

namespace Oxite.Modules.Comments.ModelBinders
{
    public class CommentInputModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            HttpRequestBase request = controllerContext.HttpContext.Request;
            string body = request.Form["body"];
            bool isAuthenticated = controllerContext.HttpContext.User.Identity.IsAuthenticated;
            bool subscribe = request.Form.IsTrue("subscribe");
            Guid parentID = Guid.Empty;

            string parentIDValue = request.Form["parentID"];
            if (!string.IsNullOrEmpty(parentIDValue))
                parentID = new Guid(parentIDValue);

            string idValue = request.Form["id"];
            if (!string.IsNullOrEmpty(idValue))
                return new CommentInput(new Guid(idValue), parentID, body, subscribe);
            else
            {
                if (isAuthenticated)
                    return new CommentInput(parentID, body, subscribe);
                else
                {
                    string creatorName = request.Form["name"];
                    string creatorEmail = request.Form["email"];
                    string creatorEmailHash = request.Form["emailHash"];
                    string creatorUrl = request.Form["url"];
                    bool saveAnonymousUser = request.Form.IsTrue("remember");

                    if (!string.IsNullOrEmpty(creatorEmail) && string.IsNullOrEmpty(creatorEmailHash))
                        creatorEmailHash = creatorEmail.ComputeHash();

                    return new CommentInput(parentID, body, subscribe, saveAnonymousUser, new UserAnonymous(creatorName, creatorEmail, creatorEmailHash, creatorUrl));
                }
            }
        }
    }
}
