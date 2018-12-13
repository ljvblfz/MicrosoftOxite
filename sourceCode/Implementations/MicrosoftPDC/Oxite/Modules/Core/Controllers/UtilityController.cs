//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Oxite.Extensions;
using Oxite.Models;
using Oxite.Services;
using Oxite.ViewModels;

namespace Oxite.Modules.Core.Controllers
{
    public class UtilityController : Controller
    {
        public ViewResult OpenSearch()
        {
            return View(new OxiteViewModel());
        }

        public ViewResult OpenSearchOSDX()
        {
            return View(new OxiteViewModel());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ContentResult ComputeEmailHash(string value)
        {
            return Content(value.ComputeEmailHash(), "text/plain");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ContentResult GetDateTime(string dateTimeString)
        {
            DateTime dateTime = default(DateTime);

            if (DateTime.TryParse(dateTimeString, out dateTime))
                return Content(dateTime.ToStringForEdit(), "text/plain");

            return Content(DateTime.Now.ToStringForEdit(), "text/plain");
        }

        public OxiteViewModel RobotsTxt()
        {
            return new OxiteViewModel();
        }
    }
}
