//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Linq;
using Oxite.Controllers;
using Oxite.Infrastructure;

namespace Oxite.ActionFilters
{
    public class OxiteRegisterActionFilters : IRegisterActionFilters
    {
        #region IRegisterFilters Members

        public void RegisterFilters(IActionFilterRegistry actionFilterRegistry)
        {
            actionFilterRegistry.Add(Enumerable.Empty<IActionFilterCriteria>(), typeof(SiteInfoActionFilter));
            actionFilterRegistry.Add(Enumerable.Empty<IActionFilterCriteria>(), typeof(UserActionFilter));
            actionFilterRegistry.Add(Enumerable.Empty<IActionFilterCriteria>(), typeof(LocalizationActionFilter));
            actionFilterRegistry.Add(Enumerable.Empty<IActionFilterCriteria>(), typeof(AntiForgeryAuthorizationFilter));
            actionFilterRegistry.Add(Enumerable.Empty<IActionFilterCriteria>(), typeof(SkinResultFilter));

            //TODO: (erikpo) Check a ViewTrackingEnabled Site setting to add this or not
            actionFilterRegistry.Add(Enumerable.Empty<IActionFilterCriteria>(), typeof(ViewTrackingResultFilter));

            actionFilterRegistry.Add(new[] { new DataFormatCriteria("RSS") }, typeof(RssResultActionFilter));
            actionFilterRegistry.Add(new[] { new DataFormatCriteria("ATOM") }, typeof(AtomResultActionFilter));

            ControllerActionCriteria listActionsCriteria = new ControllerActionCriteria();
            listActionsCriteria.AddMethod<AreaController>(a => a.Find());
            listActionsCriteria.AddMethod<AreaController>(a => a.FindQuery(null));
            listActionsCriteria.AddMethod<AreaController>(a => a.BlogML(null));
            listActionsCriteria.AddMethod<AreaController>(a => a.BlogMLSave(null, null, null));
            listActionsCriteria.AddMethod<CommentController>(c => c.List(0, 0));
            listActionsCriteria.AddMethod<PostController>(p => p.List(null, 0, null));
            listActionsCriteria.AddMethod<PostController>(p => p.ListByArchive(0, null));
            listActionsCriteria.AddMethod<PostController>(p => p.ListByArea(null, 0, null, null));
            listActionsCriteria.AddMethod<PostController>(p => p.ListBySearch(null, 0, null, null));
            listActionsCriteria.AddMethod<PostController>(p => p.ListByTag(null, 0, null, null));
            listActionsCriteria.AddMethod<PostController>(p => p.ListWithDrafts(null, 0));
            actionFilterRegistry.Add(new[] { listActionsCriteria }, typeof(ArchiveListActionFilter));
            actionFilterRegistry.Add(new[] { listActionsCriteria }, typeof(PageSizeActionFilter));

            ControllerActionCriteria itemActionCriteria = new ControllerActionCriteria();
            itemActionCriteria.AddMethod<PostController>(p => p.Item(null));
            itemActionCriteria.AddMethod<PostController>(p => p.AddComment(null, null, null, null, null, null));
            actionFilterRegistry.Add(new[] { itemActionCriteria }, typeof(CommentingDisabledActionFilter));

            ControllerActionCriteria tagCloudActionCriteria = new ControllerActionCriteria();
            tagCloudActionCriteria.AddMethod<TagController>(t => t.Cloud());
            actionFilterRegistry.Add(new[] { tagCloudActionCriteria }, typeof(ArchiveListActionFilter));

            ControllerActionCriteria areaListActionCriteria = new ControllerActionCriteria();
            areaListActionCriteria.AddMethod<PostController>(p => p.Add(null, null));
            areaListActionCriteria.AddMethod<PostController>(p => p.SaveAdd(null, null, null));
            areaListActionCriteria.AddMethod<PostController>(p => p.Edit(null));
            areaListActionCriteria.AddMethod<PostController>(p => p.SaveEdit(null, null));
            actionFilterRegistry.Add(new[] { areaListActionCriteria }, typeof(AreaListActionFilter));

            ControllerActionCriteria pageListActionCriteria = new ControllerActionCriteria();
            pageListActionCriteria.AddMethod<PageController>(p => p.Add(null));
            pageListActionCriteria.AddMethod<PageController>(p => p.SaveAdd(null, null, null));
            pageListActionCriteria.AddMethod<PageController>(p => p.Edit(null));
            pageListActionCriteria.AddMethod<PageController>(p => p.SaveEdit(null, null, null));
            actionFilterRegistry.Add(new[] { pageListActionCriteria }, typeof(PageListActionFilter));

            ControllerActionCriteria adminActionsCriteria = new ControllerActionCriteria();
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
            adminActionsCriteria.AddMethod<PluginController>(p => p.Item(Guid.Empty));
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
            actionFilterRegistry.Add(new[] { adminActionsCriteria }, typeof(AuthorizationFilter));

            ControllerActionCriteria dashboardDataActionCriteria = new ControllerActionCriteria();
            dashboardDataActionCriteria.AddMethod<SiteController>(s => s.Dashboard());
            actionFilterRegistry.Add(new[] { dashboardDataActionCriteria }, typeof(DashboardDataActionFilter));
        }

        #endregion
    }
}
