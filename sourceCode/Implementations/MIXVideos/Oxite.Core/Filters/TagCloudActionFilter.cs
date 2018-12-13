//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Services;
using Oxite.ViewModels;
using Oxite.Models;

namespace Oxite.Filters
{
    public class TagCloudActionFilter : IActionFilter
    {
        private readonly ITagService tagService;
        private readonly IAreaService areaService;

        public TagCloudActionFilter(ITagService tagService, IAreaService areaService)
        {
            this.tagService = tagService;
            this.areaService = areaService;
        }

        #region IActionFilter Members

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            OxiteModel model = filterContext.Controller.ViewData.Model as OxiteModel;

            if (model != null)
            {
                string areaName = filterContext.RouteData.Values["areaName"] as string;
                if (areaName != null)
                {
                    Area area = areaService.GetArea(areaName);
                    model.AddModelItem(new TagCloudViewModel(tagService.GetTagsUsedIn(area)));
                }
                else
                {
                    model.AddModelItem(new TagCloudViewModel(tagService.GetTagsWithPostCount()));
                }
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        #endregion
    }
}
