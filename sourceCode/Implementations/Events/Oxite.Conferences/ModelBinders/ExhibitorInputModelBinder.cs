//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Web.Mvc;
using Oxite.Modules.Conferences.Models;

namespace Oxite.Modules.Conferences.ModelBinders
{
    public class ExhibitorInputModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var request = controllerContext.HttpContext.Request;

            var id = request.Form["id"];
            var name = request.Form["name"];
            var participantLevel = request.Form["participantLevel"];
            var description = request.Form["description"];
            var siteUrl = request.Form["siteUrl"];
            var logoUrl = request.Form["logoUrl"];
            var contactName = request.Form["contactName"];
            var contactEmail = request.Form["contactEmail"];
            var location = request.Form["location"];
            var tags = request.Form["tags"];
            
            return new ExhibitorInput(new Guid(id),
                                      name,
                                      participantLevel,
                                      siteUrl,
                                      logoUrl,
                                      description,
                                      contactName,
                                      contactEmail,
                                      location,
                                      tags);
        }
    }
}
