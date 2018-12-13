//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Linq;
using MIXVideos.Oxite.Controllers;
using MIXVideos.Oxite.Filters;
using Oxite.Controllers;
using Oxite.Filters;
using Oxite.Infrastructure;

namespace MIXVideos.Oxite.Infrastructure
{
    public class MIXVideosRegisterFilters : IRegisterFilters
    {
        #region IRegisterFilters Members

        public void RegisterFilters(IFilterRegistry registry)
        {
            registry.Add(Enumerable.Empty<IFilterCriteria>(), typeof(AreaListActionFilter));
            registry.Add(Enumerable.Empty<IFilterCriteria>(), typeof(TagCloudActionFilter));
            registry.Add(Enumerable.Empty<IFilterCriteria>(), typeof(SidebarActionFilter));

            ControllerActionFilterCriteria itemActionCriteria = new ControllerActionFilterCriteria();
            itemActionCriteria.AddMethod<PostController>(p => p.Item(null));
            itemActionCriteria.AddMethod<PostController>(p => p.AddComment(null, null, null, null, null, null));
            registry.Add(new[] { itemActionCriteria }, typeof(PostWebViewBugResultFilter));

            ControllerActionFilterCriteria listActionCriteria = new ControllerActionFilterCriteria();
            listActionCriteria.AddMethod<PostController>(p => p.List(null, 0, null));
            listActionCriteria.AddMethod<PostController>(p => p.ListByArchive(0, null));
            listActionCriteria.AddMethod<PostController>(p => p.ListByArea(null, 0, null, null));
            listActionCriteria.AddMethod<PostController>(p => p.ListBySearch(null, 0, null, null));
            listActionCriteria.AddMethod<PostController>(p => p.ListByTag(null, 0, null, null));
            registry.Add(new IFilterCriteria[] { listActionCriteria, new DataFormatFilterCriteria("RSS") }, typeof(PostRssViewBugResultFilter));
            registry.Add(new IFilterCriteria[] { listActionCriteria, new DataFormatFilterCriteria("ATOM") }, typeof(PostAtomViewBugResultFilter));

            ControllerActionFilterCriteria listActionsCriteria = new ControllerActionFilterCriteria();
            listActionsCriteria.AddMethod<PostController>(p => p.List(null, 0, null));
            listActionsCriteria.AddMethod<PostController>(p => p.ListByArchive(0, null));
            listActionsCriteria.AddMethod<PostController>(p => p.ListByArea(null, 0, null, null));
            listActionsCriteria.AddMethod<PostController>(p => p.ListByAreaAndTag(null, 0, null, null, null));
            listActionsCriteria.AddMethod<PostController>(p => p.ListBySearch(null, 0, null, null));
            listActionsCriteria.AddMethod<PostController>(p => p.ListByTag(null, 0, null, null));
            listActionsCriteria.AddMethod<PostController>(p => p.ListWithDrafts(null, 0));
            registry.Add(new[] { listActionsCriteria }, typeof(MIXVideos.Oxite.Filters.PageSizeActionFilter));

            ControllerActionFilterCriteria outputCacheCriteria = new ControllerActionFilterCriteria();
            outputCacheCriteria.AddMethod<PostController>(c => c.List(null, 0, null));
            outputCacheCriteria.AddMethod<PostController>(c => c.ListByArchive(0, null));
            outputCacheCriteria.AddMethod<PostController>(c => c.ListByArea(null, 0, null, null));
            outputCacheCriteria.AddMethod<PostController>(c => c.ListBySearch(null, 0, null, null));
            outputCacheCriteria.AddMethod<PostController>(c => c.ListByTag(null, 0, null, null));
            outputCacheCriteria.AddMethod<TagController>(c => c.Cloud());
            outputCacheCriteria.AddMethod<TagController>(c => c.CloudForArea(null));
            registry.Add(new[] { outputCacheCriteria }, typeof(OneHourOutputCacheFilter));

            ControllerActionFilterCriteria fileFeedActionCriteria = new ControllerActionFilterCriteria();
            fileFeedActionCriteria.AddMethod<FeedController>(f => f.List(null));
            registry.Add(new IFilterCriteria[] { fileFeedActionCriteria, new DataFormatFilterCriteria("RSS") }, typeof(PostRssViewBugResultFilter));
        }

        #endregion
    }
}
