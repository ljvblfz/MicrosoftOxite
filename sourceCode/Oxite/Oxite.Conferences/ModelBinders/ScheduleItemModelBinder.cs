//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Modules.Conferences.Models;
using Oxite.Modules.Conferences.Services;

namespace Oxite.Modules.Conferences.ModelBinders
{
    public class ScheduleItemModelBinder : IModelBinder
    {
        private readonly IEventService eventService;
        private readonly IScheduleItemService scheduleItemService;

        public ScheduleItemModelBinder(IEventService eventService, IScheduleItemService scheduleItemService)
        {
            this.eventService = eventService;
            this.scheduleItemService = scheduleItemService;
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            string eventName = bindingContext.ValueProvider.ContainsKey("eventName") ? (string)bindingContext.ValueProvider["eventName"].RawValue : null;

            if (!string.IsNullOrEmpty(eventName))
            {
                Event evnt = eventService.GetEvent(eventName);

                if (evnt != null)
                {
                    string scheduleItemSlug = bindingContext.ValueProvider.ContainsKey("scheduleItemSlug") ? (string)bindingContext.ValueProvider["scheduleItemSlug"].RawValue : null;

                    if (!string.IsNullOrEmpty(scheduleItemSlug))
                        return scheduleItemService.GetScheduleItem(evnt, scheduleItemSlug);
                }
            }

            return null;
        }
    }
}