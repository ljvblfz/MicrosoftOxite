//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Web;
using System.Web.Mvc;
using Microsoft.Passport.RPS;

namespace Oxite.Modules.LiveID.Controllers
{
    public class UserController : Controller
    {
        public ActionResult SignOutImage()
        {
            clearLiveIDCookies();

            return File(Convert.FromBase64String("R0lGODlhAQABAIAAANvf7wAAACH5BAEAAAAALAAAAAABAAEAAAICRAEAOw=="), "image/gif");
        }

        private void clearLiveIDCookies()
        {
            HttpResponse response = (HttpResponse)HttpContext.Items["originalResponse"];
            RPS myRps = (RPS)HttpContext.Application["globalRPS"];
            string siteDnsName = HttpContext.Request.ServerVariables["SERVER_NAME"];

            RPSHttpAuth rpsHttpAuth = new RPSHttpAuth(myRps);

            string rLogoutHeaders = rpsHttpAuth.GetLogoutHeaders(siteDnsName);
            rpsHttpAuth.WriteHeaders(response, rLogoutHeaders);
        }
    }
}
