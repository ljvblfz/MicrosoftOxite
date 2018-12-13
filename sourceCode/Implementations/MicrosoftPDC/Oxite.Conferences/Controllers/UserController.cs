//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Conferences.Models;
using Oxite.Modules.Conferences.Services;
using Oxite.Modules.Membership.Services;
using Oxite.ViewModels;

namespace Oxite.Modules.Conferences.Controllers
{
    public class UserController : Controller
    {
        private readonly IScheduleItemService scheduleItemService;
        private readonly IUserScheduleService userScheduleService;
        private readonly IUserService userService;
        private readonly OxiteContext context; 

        public UserController(IScheduleItemService scheduleItemService, IUserScheduleService userScheduleService, IUserService userService, OxiteContext context)
        {
            this.scheduleItemService = scheduleItemService;
            this.userScheduleService = userScheduleService;
            this.userService = userService;
            this.context = context;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Schedule(int pageIndex, int pageSize, EventAddress eventAddress, string userName)
        {
            var scheduleUser = userService.GetUser(userName);

            if(scheduleUser == null)
            {
                // No user found
                return null;
            }
            
            var userID = scheduleUser.ID;

            var user = context.User != null ? context.User.Cast<UserAuthenticated>() : null;

            var userScheduleIsPublic = userScheduleService.IsUserSchedulePublic(userID);
            var showSchedule = user != null
                                   ? context.User.Name.Equals(userName)
                                         ? true
                                         : userScheduleIsPublic
                                   : userScheduleIsPublic;

            var scheduleItems = showSchedule
                                    ? scheduleItemService.GetScheduleItemsByUser(pageIndex,
                                                                                 pageSize,
                                                                                 eventAddress,
                                                                                 userID)
                                    : new PageOfItems<ScheduleItem>(new List<ScheduleItem>(0), 0, 0, 0);

            ViewData["UserDisplayName"] = scheduleUser.DisplayName;
            ViewData["UserScheduleIsPublic"] = userScheduleIsPublic; 

            return View(new OxiteViewModelItems<ScheduleItem>(scheduleItems));
        }
    }
}
