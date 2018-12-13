//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Modules.Conferences.Services;

namespace Oxite.Modules.Conferences.ModelBinders
{
    public class EventModelBinder : IModelBinder
    {
        private readonly IEventService eventService;

        public EventModelBinder(IEventService eventService)
        {
            this.eventService = eventService;
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            string eventName = bindingContext.ValueProvider.ContainsKey("eventName") ? (string)bindingContext.ValueProvider["eventName"].RawValue : null;

            return !string.IsNullOrEmpty(eventName) ? eventService.GetEvent(eventName) : null;
        }
    }
}