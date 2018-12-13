//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Modules.Conferences.Services;

namespace Oxite.Modules.Conferences.ModelBinders
{
    public class SpeakerModelBinder : IModelBinder
    {
        private readonly ISpeakerService speakerService;

        public SpeakerModelBinder(ISpeakerService speakerService)
        {
            this.speakerService = speakerService;
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            string speakerName = (string)bindingContext.ValueProvider["speakerName"].RawValue;

            return !string.IsNullOrEmpty(speakerName) ? speakerService.GetSpeaker(speakerName) : null;
        }
    }
}