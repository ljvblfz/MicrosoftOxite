//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Modules.Conferences.Models;

namespace Oxite.Modules.Conferences.ModelBinders
{
    public class SpeakerAddressModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            string speakerName = controllerContext.RouteData.Values["speakerName"] as string;

            if (!string.IsNullOrEmpty(speakerName))
                return new SpeakerAddress(speakerName);

            return null;
        }
    }
}