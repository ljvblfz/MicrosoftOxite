//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Modules.Conferences.Models;
using Oxite.Modules.Conferences.Services;
using Oxite.ViewModels;
using OxiteSite.App_Code.Modules.OxiteSite.ViewModels;

namespace OxiteSite.App_Code.Modules.OxiteSite.Filters
{
    public class ScheduleItemsTagListActionFilter : IActionFilter
    {
        private readonly IScheduleItemService scheduleItemService;

        public ScheduleItemsTagListActionFilter(IScheduleItemService scheduleItemService)
        {
            this.scheduleItemService = scheduleItemService;
        }

        #region IActionFilter Members

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            OxiteViewModel model = filterContext.Controller.ViewData.Model as OxiteViewModel;

            if (model != null)
            {
                string eventName = filterContext.RouteData.Values["eventName"] as string;

                if (eventName != null)
                    model.AddModelItem(new TagListViewModel(scheduleItemService.GetScheduleItemTags(new EventAddress(eventName))));
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext) { }

        #endregion
    }
}
