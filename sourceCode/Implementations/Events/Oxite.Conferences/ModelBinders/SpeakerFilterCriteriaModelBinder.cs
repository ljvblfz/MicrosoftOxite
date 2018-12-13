//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Modules.Conferences.Models;

namespace Oxite.Modules.Conferences.ModelBinders
{
    public class SpeakerFilterCriteriaModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            SpeakerFilterCriteria speakerFilterCriteria = 
                new SpeakerFilterCriteria(controllerContext.RouteData.Values["speakerFilterCriteria"] as string);

            if (string.IsNullOrEmpty(speakerFilterCriteria.Term) &&
                !string.IsNullOrEmpty(controllerContext.HttpContext.Request.QueryString["term"]))
                speakerFilterCriteria.Term =
                    controllerContext.HttpContext.Request.QueryString["term"];

            return speakerFilterCriteria;
        }
    }
}