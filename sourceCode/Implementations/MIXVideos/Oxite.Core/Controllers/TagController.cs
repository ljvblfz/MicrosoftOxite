//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Oxite.Models;
using Oxite.Services;
using Oxite.ViewModels;

namespace Oxite.Controllers
{
    public class TagController : Controller
    {
        private readonly ITagService tagService;
        private readonly IAreaService areaService;

        public TagController(ITagService tagService, IAreaService areaService)
        {
            this.tagService = tagService;
            this.areaService = areaService;
        }

        public virtual OxiteModelList<KeyValuePair<Tag, int>> Cloud()
        {
            return getTagList(new TagCloudPageContainer(), () => tagService.GetTagsWithPostCount());
        }

        public virtual OxiteModelList<KeyValuePair<Tag, int>> CloudForArea(Area areaInput)
        {
            Area area = areaService.GetArea(areaInput.Name);

            if (area == null)
                return null;

            return getTagList(area, () => tagService.GetTagsUsedIn(area));
        }

        private static OxiteModelList<KeyValuePair<Tag, int>> getTagList(INamedEntity container, Func<IList<KeyValuePair<Tag, int>>> serviceCall)
        {
            var result = new OxiteModelList<KeyValuePair<Tag, int>>
            {
                Container = container,
                List = serviceCall()
            };

            return result;
        }
    }
}