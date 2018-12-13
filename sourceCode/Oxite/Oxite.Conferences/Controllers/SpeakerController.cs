// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using System.Web.Mvc;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Conferences.Models;
using Oxite.Modules.Conferences.Services;
using Oxite.Services;
using Oxite.ViewModels;

namespace Oxite.Modules.Conferences.Controllers
{
    public class SpeakerController : Controller
    {
        private readonly IEventService eventService;
        private readonly ISpeakerService speakerService;
        private readonly IScheduleItemService scheduleItemService;

        public SpeakerController(IEventService eventService, ISpeakerService speakerService, IScheduleItemService scheduleItemService)
        {
            this.eventService = eventService;
            this.speakerService = speakerService;
            this.scheduleItemService = scheduleItemService;

            ValidateRequest = false;
        }

        public OxiteViewModelItems<Speaker> List(SpeakerFilterCriteria speakerFilterCriteria)
        {
            return new OxiteViewModelItems<Speaker>(speakerService.GetSpeakers(speakerFilterCriteria));
        }

        public OxiteViewModelItems<Speaker> ListByEvent(Event evnt, SpeakerFilterCriteria speakerFilterCriteria)
        {
            if (evnt == null) return null;

            IPageOfItems<Speaker> speakers = speakerService.GetSpeakers(evnt, speakerFilterCriteria);

            return new OxiteViewModelItems<Speaker>(speakers) { Container = evnt };
        }

        public OxiteViewModelItemItems<Speaker, ScheduleItem> Item(Speaker speaker)
        {
            if (speaker == null) return null;

            IPageOfItems<ScheduleItem> scheduleItems = scheduleItemService.GetScheduleItems(new ScheduleItemFilterCriteria { SpeakerName = speaker.Name });

            return new OxiteViewModelItemItems<Speaker, ScheduleItem>(speaker, scheduleItems);
        }
    }
}
