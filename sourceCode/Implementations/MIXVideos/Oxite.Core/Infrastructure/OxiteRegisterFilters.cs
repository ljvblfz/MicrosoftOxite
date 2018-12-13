//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Linq;
using Oxite.Controllers;
using Oxite.Filters;

namespace Oxite.Infrastructure
{
    public class OxiteRegisterFilters : IRegisterFilters
    {
        #region IRegisterFilters Members

        public void RegisterFilters(IFilterRegistry filterRegistry)
        {
            filterRegistry.Add(Enumerable.Empty<IFilterCriteria>(), typeof(EnsureModelExceptionFilter));
            filterRegistry.Add(Enumerable.Empty<IFilterCriteria>(), typeof(DebugActionFilter));
            filterRegistry.Add(Enumerable.Empty<IFilterCriteria>(), typeof(SiteActionFilter));
            filterRegistry.Add(Enumerable.Empty<IFilterCriteria>(), typeof(UserActionFilter));
            filterRegistry.Add(Enumerable.Empty<IFilterCriteria>(), typeof(LocalizationActionFilter));
            filterRegistry.Add(Enumerable.Empty<IFilterCriteria>(), typeof(AntiForgeryAuthorizationFilter));
            filterRegistry.Add(Enumerable.Empty<IFilterCriteria>(), typeof(AreaSkinLayerResultFilter));
            filterRegistry.Add(Enumerable.Empty<IFilterCriteria>(), typeof(SkinResultFilter));
            filterRegistry.Add(Enumerable.Empty<IFilterCriteria>(), typeof(ErrorExceptionFilter));

            filterRegistry.Add(new[] { new DataFormatFilterCriteria("RSS") }, typeof(RssResultActionFilter));
            filterRegistry.Add(new[] { new DataFormatFilterCriteria("ATOM") }, typeof(AtomResultActionFilter));

            ControllerActionFilterCriteria listActionsCriteria = new ControllerActionFilterCriteria();
            listActionsCriteria.AddMethod<AreaController>(a => a.Find());
            listActionsCriteria.AddMethod<AreaController>(a => a.FindQuery(null));
            listActionsCriteria.AddMethod<AreaController>(a => a.BlogML(null));
            listActionsCriteria.AddMethod<AreaController>(a => a.BlogMLSave(null, null, null));
            listActionsCriteria.AddMethod<CommentController>(c => c.List(0, 0));
            listActionsCriteria.AddMethod<PostController>(p => p.List(null, 0, null));
            listActionsCriteria.AddMethod<PostController>(p => p.ListByArchive(0, null));
            listActionsCriteria.AddMethod<PostController>(p => p.ListByArea(null, 0, null, null));
            listActionsCriteria.AddMethod<PostController>(p => p.ListByAreaAndTag(null, 0, null, null, null));
            listActionsCriteria.AddMethod<PostController>(p => p.ListBySearch(null, 0, null, null));
            listActionsCriteria.AddMethod<PostController>(p => p.ListByTag(null, 0, null, null));
            listActionsCriteria.AddMethod<PostController>(p => p.ListWithDrafts(null, 0));
            filterRegistry.Add(new[] { listActionsCriteria }, typeof(ArchiveListActionFilter));
            filterRegistry.Add(new[] { listActionsCriteria }, typeof(PageSizeActionFilter));

            ControllerActionFilterCriteria itemActionCriteria = new ControllerActionFilterCriteria();
            itemActionCriteria.AddMethod<PostController>(p => p.Item(null));
            itemActionCriteria.AddMethod<PostController>(p => p.AddComment(null, null, null, null, null, null));
            filterRegistry.Add(new[] { itemActionCriteria }, typeof(CommentingDisabledActionFilter));

            ControllerActionFilterCriteria tagCloudActionCriteria = new ControllerActionFilterCriteria();
            tagCloudActionCriteria.AddMethod<TagController>(t => t.Cloud());
            tagCloudActionCriteria.AddMethod<TagController>(t => t.CloudForArea(null));
            filterRegistry.Add(new[] { tagCloudActionCriteria }, typeof(ArchiveListActionFilter));

            ControllerActionFilterCriteria areaListActionCriteria = new ControllerActionFilterCriteria();
            areaListActionCriteria.AddMethod<PostController>(p => p.Add(null, null));
            areaListActionCriteria.AddMethod<PostController>(p => p.SaveAdd(null, null, null));
            areaListActionCriteria.AddMethod<PostController>(p => p.Edit(null));
            areaListActionCriteria.AddMethod<PostController>(p => p.SaveEdit(null, null));
            filterRegistry.Add(new[] { areaListActionCriteria }, typeof(AreaListActionFilter));

            ControllerActionFilterCriteria pageListActionCriteria = new ControllerActionFilterCriteria();
            pageListActionCriteria.AddMethod<PageController>(p => p.Add(null));
            pageListActionCriteria.AddMethod<PageController>(p => p.SaveAdd(null, null, null));
            pageListActionCriteria.AddMethod<PageController>(p => p.Edit(null));
            pageListActionCriteria.AddMethod<PageController>(p => p.SaveEdit(null, null, null));
            filterRegistry.Add(new[] { pageListActionCriteria }, typeof(PageListActionFilter));

            ControllerActionFilterCriteria adminActionsCriteria = new ControllerActionFilterCriteria();
            adminActionsCriteria.AddMethod<AreaController>(a => a.Find());
            adminActionsCriteria.AddMethod<AreaController>(a => a.FindQuery(null));
            adminActionsCriteria.AddMethod<AreaController>(a => a.ItemEdit(null));
            adminActionsCriteria.AddMethod<AreaController>(a => a.ItemSave(null));
            adminActionsCriteria.AddMethod<AreaController>(a => a.BlogML(null));
            adminActionsCriteria.AddMethod<AreaController>(a => a.BlogMLSave(null, null, null));
            adminActionsCriteria.AddMethod<CommentController>(c => c.List(0, 0));
            adminActionsCriteria.AddMethod<CommentController>(c => c.Remove(null, null, null));
            adminActionsCriteria.AddMethod<CommentController>(c => c.Approve(null, null, null));
            adminActionsCriteria.AddMethod<FileController>(f => f.ListByPost(null));
            adminActionsCriteria.AddMethod<FileController>(f => f.AddFileContentToPost(null, null, null));
            adminActionsCriteria.AddMethod<FileController>(f => f.AddFileToPost(null, null, null));
            adminActionsCriteria.AddMethod<FileController>(f => f.EditFileOnPost(null, null, null, null));
            adminActionsCriteria.AddMethod<FileController>(f => f.RemoveFileFromPost(null, null, null));
            adminActionsCriteria.AddMethod<PageController>(p => p.Add(null));
            adminActionsCriteria.AddMethod<PageController>(p => p.SaveAdd(null, null, null));
            adminActionsCriteria.AddMethod<PageController>(p => p.Edit(null));
            adminActionsCriteria.AddMethod<PageController>(p => p.SaveEdit(null, null, null));
            adminActionsCriteria.AddMethod<PageController>(p => p.Remove(null, null));
            adminActionsCriteria.AddMethod<PluginController>(p => p.List());
            //adminActionsCriteria.AddMethod<PluginController>(p => p.Item(Guid.Empty));
            adminActionsCriteria.AddMethod<PostController>(p => p.Add(null, null));
            adminActionsCriteria.AddMethod<PostController>(p => p.SaveAdd(null, null, null));
            adminActionsCriteria.AddMethod<PostController>(p => p.Edit(null));
            adminActionsCriteria.AddMethod<PostController>(p => p.SaveEdit(null, null));
            adminActionsCriteria.AddMethod<PostController>(p => p.Remove(null, null));
            adminActionsCriteria.AddMethod<PostController>(p => p.ListWithDrafts(null, 0));
            adminActionsCriteria.AddMethod<SiteController>(s => s.Dashboard());
            adminActionsCriteria.AddMethod<SiteController>(s => s.Item());
            
            //TODO: (erikpo) Once we have roles other than "authenticated" this should move to not be part of the admin, but just part of authed users
            adminActionsCriteria.AddMethod<UserController>(u => u.ChangePassword(null));
            filterRegistry.Add(new[] { adminActionsCriteria }, typeof(AuthorizationFilter));

            ControllerActionFilterCriteria dashboardDataActionCriteria = new ControllerActionFilterCriteria();
            dashboardDataActionCriteria.AddMethod<SiteController>(s => s.Dashboard());
            filterRegistry.Add(new[] { dashboardDataActionCriteria }, typeof(DashboardDataActionFilter));

            ControllerActionFilterCriteria spamFilterCriteria = new ControllerActionFilterCriteria();
            spamFilterCriteria.AddMethod<PostController>(p => p.AddComment(null, null, null, null, null, null));
            filterRegistry.Add(new[] { spamFilterCriteria }, typeof(SpamFilterActionFilter));
        }

        #endregion
    }
}
