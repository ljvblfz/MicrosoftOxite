//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Web.Mvc;
using Oxite.Modules.Conferences.Models;

namespace OxiteSite.App_Code.Modules.OxiteSite.ModelBinder
{
    public class DateRangeAddressModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            string dayName = controllerContext.RouteData.Values["dayName"] as string;

            if (string.Compare(dayName, "Thursday", true) == 0)
                return new DateRangeAddress(new DateTime(2009, 11, 19, 0, 0, 0), new DateTime(2009, 11, 19, 23, 59, 59));

            if (string.Compare(dayName, "Wednesday", true) == 0)
                return new DateRangeAddress(new DateTime(2009, 11, 18, 0, 0, 0), new DateTime(2009, 11, 18, 23, 59, 59));

            if (string.Compare(dayName, "Tuesday", true) == 0)
                return new DateRangeAddress(new DateTime(2009, 11, 17, 0, 0, 0), new DateTime(2009, 11, 17, 23, 59, 59));

            return new DateRangeAddress(new DateTime(2009, 11, 16, 0, 0, 0), new DateTime(2009, 11, 16, 23, 59, 59)); // <- todo: (nheskew) during the conference use the current date
        }
    }
}