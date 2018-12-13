//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Modules.Conferences.Models;

namespace Oxite.Modules.Conferences.ModelBinders
{
    public class ScheduleItemCommentAddressModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            return new ScheduleItemCommentAddress(
                new ScheduleItemAddress(
                    new EventAddress(
                        controllerContext.RouteData.GetRequiredString("eventName")
                        ),
                    controllerContext.RouteData.GetRequiredString("scheduleItemSlug")
                    ),
                controllerContext.RouteData.GetRequiredString("commentSlug")
                );
        }
    }
}
