//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using System.Web.Mvc;
using Oxite.Models;
using Oxite.Services;
using Oxite.ViewModels;

namespace Oxite.Filters
{
    public class ArchiveListActionFilter : IActionFilter
    {
        private readonly IPostService postService;
        private readonly IAreaService areaService;

        public ArchiveListActionFilter(IPostService postService, IAreaService areaService)
        {
            this.postService = postService;
            this.areaService = areaService;
        }

        #region IActionFilter Members

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            OxiteModel model = filterContext.Controller.ViewData.Model as OxiteModel;

            if (model != null)
            {
                IList<KeyValuePair<ArchiveData, int>> archives;
                INamedEntity container;
                string areaName = filterContext.RouteData.Values["areaName"] as string;

                if (!string.IsNullOrEmpty(areaName))
                {
                    archives = postService.GetArchives(new Area { Name = areaName });
                    container = areaService.GetArea(areaName);
                }
                else
                {
                    archives = postService.GetArchives();
                    container = new HomePageContainer();
                }

                model.AddModelItem(new ArchiveViewModel(archives, container));
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        #endregion
    }
}
