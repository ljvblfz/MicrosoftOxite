// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using System.Collections.Generic;
using System.Web.Mvc;
using Oxite.Infrastructure;
using Oxite.Modules.Conferences.Models;
using Oxite.Modules.Conferences.Services;
using Oxite.Modules.Membership.Services;
using Oxite.ViewModels;

namespace OxiteSite.App_Code.Modules.OxiteSite.Controllers
{
    public class ScheduleItemController : Controller
    {
        private readonly IEventService eventService;
        private readonly IScheduleItemService scheduleItemService;
        private readonly IUserService userService;
        private readonly IUser currentUser;

        public ScheduleItemController(IEventService eventService, IScheduleItemService scheduleItemService, IUserService userService, IUser user)
        {
            this.eventService = eventService;
            this.scheduleItemService = scheduleItemService;
            this.userService = userService;
            this.currentUser = user;
            ValidateRequest = false;
        }

        public OxiteViewModelItems<ScheduleItem> ListByDateRange(EventAddress eventAddress, DateRangeAddress dateRangeAddress)
        {
            Event evnt = eventService.GetEvent(eventAddress);

            if (evnt == null) return null;

            IEnumerable<ScheduleItem> scheduleItems = scheduleItemService.GetScheduleItemsByTimeSlot(eventAddress, dateRangeAddress);

            return new OxiteViewModelItems<ScheduleItem>(scheduleItems) { Container = evnt };
        }

        public OxiteViewModelItems<ScheduleItem> ListByDateRangeAndUser(EventAddress eventAddress, DateRangeAddress dateRangeAddress)
        {
            Event evnt = eventService.GetEvent(eventAddress);

            if (evnt == null) return null;

            var userAuthenticated = userService.GetUser(currentUser.Name);
            if(userAuthenticated == null)
            {
                return null;
            }

            var userID = userAuthenticated.ID;
            
            IEnumerable<ScheduleItem> scheduleItems = scheduleItemService.GetScheduleItemsByTimeSlotAndUser(eventAddress, dateRangeAddress, userID);

            return new OxiteViewModelItems<ScheduleItem>(scheduleItems) { Container = evnt };
        }

        //public OxiteViewModelItems<ScheduleItem> SiteMap(EventAddress eventAddress)
        //{
        //    Event evnt = eventService.GetEvent(eventAddress);

        //    if (evnt == null) return null;

        //    return
        //        new OxiteViewModelItems<ScheduleItem>(
        //            scheduleItemService.GetScheduleItems(eventAddress,new ScheduleItemFilterCriteria()));

        //}


    }
}
