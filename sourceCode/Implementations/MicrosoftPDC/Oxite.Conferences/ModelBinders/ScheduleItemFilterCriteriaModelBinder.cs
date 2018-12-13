//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Modules.Conferences.Models;

namespace Oxite.Modules.Conferences.ModelBinders
{
    public class ScheduleItemFilterCriteriaModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            ScheduleItemFilterCriteria scheduleItemFilterCriteria =
                new ScheduleItemFilterCriteria(controllerContext.RouteData.Values["scheduleItemFilterCriteria"] as string);

            if (string.IsNullOrEmpty(scheduleItemFilterCriteria.ScheduleItemType) &&
                controllerContext.RouteData.Values.ContainsKey("scheduleItemType"))
                scheduleItemFilterCriteria.ScheduleItemType =
                    controllerContext.RouteData.Values["scheduleItemType"] as string;

            if (string.IsNullOrEmpty(scheduleItemFilterCriteria.Term) &&
                !string.IsNullOrEmpty(controllerContext.HttpContext.Request.QueryString["term"]))
                scheduleItemFilterCriteria.Term =
                    controllerContext.HttpContext.Request.QueryString["term"];

            return scheduleItemFilterCriteria;
        }
    }
}