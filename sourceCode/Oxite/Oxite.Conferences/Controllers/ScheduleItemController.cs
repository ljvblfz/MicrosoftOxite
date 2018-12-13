// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using System;
using System.Web.Mvc;
using Oxite.Extensions;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Comments.Models;
using Oxite.Modules.Conferences.Extensions;
using Oxite.Modules.Conferences.Models;
using Oxite.Modules.Conferences.Services;
using Oxite.Modules.Tags.Models;
using Oxite.Modules.Tags.Services;
using Oxite.ViewModels;

namespace Oxite.Modules.Conferences.Controllers
{
    public class ScheduleItemController : Controller
    {
        private readonly IEventService eventService;
        private readonly IScheduleItemService scheduleItemService;
        private readonly ITagService tagService;
        private readonly IConferencesCommentService commentService;
        private readonly OxiteContext context;

        public ScheduleItemController(IEventService eventService, IScheduleItemService scheduleItemService, ITagService tagService, IConferencesCommentService commentService, OxiteContext context)
        {
            this.eventService = eventService;
            this.scheduleItemService = scheduleItemService;
            this.tagService = tagService;
            this.commentService = commentService;
            this.context = context;
            ValidateRequest = false;
        }

        public OxiteViewModelItems<ScheduleItem> List(ScheduleItemFilterCriteria scheduleItemFilterCriteria)
        {
            return new OxiteViewModelItems<ScheduleItem>(scheduleItemService.GetScheduleItems(scheduleItemFilterCriteria));
        }

        public OxiteViewModelItems<ScheduleItem> ListByEvent(Event evnt, ScheduleItemFilterCriteria scheduleItemFilterCriteria)
        {
            if (evnt == null) return null;

            IPageOfItems<ScheduleItem> scheduleItems = scheduleItemService.GetScheduleItems(evnt, scheduleItemFilterCriteria);

            return new OxiteViewModelItems<ScheduleItem>(scheduleItems) { Container = evnt };
        }

        public OxiteViewModelItemItems<Tag, ScheduleItem> ListByEventAndTag(Event evnt, Tag tag, ScheduleItemFilterCriteria scheduleItemFilterCriteria)
        {
            if (evnt == null) return null;

            if (tag == null) return null;

            IPageOfItems<ScheduleItem> scheduleItems = scheduleItemService.GetScheduleItemsByTag(evnt, tag, scheduleItemFilterCriteria);

            return new OxiteViewModelItemItems<Tag, ScheduleItem>(tag, scheduleItems) { Container = evnt };
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Head)]
        public OxiteViewModelItem<ScheduleItem> Item(ScheduleItem scheduleItem)
        {
            if (scheduleItem == null) return null;

            bool includeUnapproved = context.User.IsAuthenticated && context.User.IsInRole("Admin");

            return new OxiteViewModelItem<ScheduleItem>(scheduleItem)
            {
                Container = scheduleItem.Event
            };
        }

        [ActionName("Item"), AcceptVerbs(HttpVerbs.Post)]
        public object AddComment(ScheduleItem scheduleItem, CommentInput commentInput)
        {
            if (context.Site.CommentingDisabled) return null;

            if (scheduleItem == null) return null;

            ModelResult<ScheduleItemComment> addCommentResults = commentService.AddComment(scheduleItem, commentInput);

            if (!addCommentResults.IsValid)
            {
                ModelState.AddModelErrors(addCommentResults.ValidationState);

                return Item(scheduleItem);
            }

            if (!context.User.IsAuthenticated)
            {
                if (commentInput.SaveAnonymousUser)
                    Response.Cookies.SetAnonymousUser(commentInput.Creator);
                else if (Request.Cookies.GetAnonymousUser() != null)
                    Response.Cookies.ClearAnonymousUser();
            }

            return new RedirectResult(
                addCommentResults.Item.State != EntityState.PendingApproval
                ? Url.Comment(addCommentResults.Item)
                : Url.CommentPending(addCommentResults.Item)
                );
        }
    }
}
