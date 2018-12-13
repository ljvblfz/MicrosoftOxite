//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Web.Mvc;
using Oxite.Modules.Blogs.Models;

namespace Oxite.Modules.Blogs.ModelBinders
{
    public class OneMonthDateRangeAddressModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            string yearValue = controllerContext.RouteData.GetRequiredString("year");
            int year = 0;

            if (!int.TryParse(yearValue, out year)) throw new ArgumentException("The year supplied was not a valid value");

            string monthValue = controllerContext.RouteData.GetRequiredString("month");
            int month = 0;

            if (!int.TryParse(monthValue, out month)) throw new ArgumentException("The month supplied was not a valid value");

            try
            {
                new DateTime(year, month, 1);
            }
            catch
            {
                throw new ArgumentException("The year and/or month supplied were not valid");
            }

            return new OneMonthDateRangeAddress(year, month);
        }
    }
}
