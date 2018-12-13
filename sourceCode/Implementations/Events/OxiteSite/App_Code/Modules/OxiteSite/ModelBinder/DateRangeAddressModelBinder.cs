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

            return new DateRangeAddress(new DateTime(2010, 3, 14, 0, 0, 0), new DateTime(2010, 3, 17, 23, 59, 59)); // <- todo: (nheskew) during the conference use the current date
        }
    }
}