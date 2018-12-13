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
using System.Web.Routing;

namespace Oxite.Modules.Core.Controllers
{
   public class SkinController : Controller
   {
      public ActionResult ChangeSkin(string skin)
      {
         Response.Cookies.SetSelectedSkin(skin);

         return Redirect(Request.UrlReferrer != null ? Request.UrlReferrer.ToString() : "/");
      }
   }
}
