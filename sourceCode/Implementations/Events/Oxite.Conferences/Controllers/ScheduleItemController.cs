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
using Oxite.Modules.Conferences.Filters;
using Oxite.Modules.Conferences.Models;
using Oxite.Modules.Conferences.Results;
using Oxite.Modules.Conferences.Services;
using Oxite.Modules.Tags.Models;
using Oxite.Modules.Tags.Services;
using Oxite.Results;
using Oxite.ViewModels;

namespace Oxite.Modules.Conferences.Controllers
{
    public class ScheduleItemController : Controller 
    {
        private readonly IEventService eventService;
        private readonly IScheduleItemService scheduleItemService;
        private readonly IUserScheduleService userScheduleService;
        private readonly ITagService tagService;
        private readonly IConferencesCommentService commentService;
        private readonly OxiteContext context;

        public ScheduleItemController(IEventService eventService, IScheduleItemService scheduleItemService, ITagService tagService, IConferencesCommentService commentService, IUserScheduleService userScheduleService, OxiteContext context)
        {
            this.eventService = eventService;
            this.scheduleItemService = scheduleItemService;
            this.tagService = tagService;
            this.commentService = commentService;
            this.context = context;
            this.userScheduleService = userScheduleService;
            ValidateRequest = false;
        }

        public OxiteViewModelItems<ScheduleItem> List(ScheduleItemFilterCriteria scheduleItemFilterCriteria)
        {
            SetUserScheduleStatus();

            return new OxiteViewModelItems<ScheduleItem>(scheduleItemService.GetScheduleItems(scheduleItemFilterCriteria));
        }

        public OxiteViewModelItems<ScheduleItem> ListByEvent(EventAddress eventAddress, ScheduleItemFilterCriteria scheduleItemFilterCriteria)
        {
            return ListByEventImpl(eventAddress, scheduleItemFilterCriteria);
        }

        private OxiteViewModelItems<ScheduleItem> ListByEventImpl(EventAddress eventAddress, ScheduleItemFilterCriteria scheduleItemFilterCriteria)
        {
            Event evnt = eventService.GetEvent(eventAddress);
            if (evnt == null)
            {
                return null;
            }
            
            IPageOfItems<ScheduleItem> scheduleItems = scheduleItemService.GetScheduleItems(eventAddress, scheduleItemFilterCriteria);
            
            SetUserScheduleStatus();

            return new OxiteViewModelItems<ScheduleItem>(scheduleItems) { Container = evnt };
        }

        [XmlResultFilter]
        public OxiteViewModelItems<ScheduleItem> ListByEventXml(EventAddress eventAddress, ScheduleItemFilterCriteria scheduleItemFilterCriteria)
        {
            return ListByEventImpl(eventAddress, scheduleItemFilterCriteria);
        }

        [JsonResultFilter]
        public OxiteViewModelItems<ScheduleItem> ListByEventJson(EventAddress eventAddress, ScheduleItemFilterCriteria scheduleItemFilterCriteria)
        {
            return ListByEventImpl(eventAddress, scheduleItemFilterCriteria);
        }

        public OxiteViewModelItems<ScheduleItem> ListByEventWithVideo(EventAddress eventAddress, ScheduleItemFilterCriteria scheduleItemFilterCriteria)
        {
            scheduleItemFilterCriteria.WithFileTypes.Add("video");
            scheduleItemFilterCriteria.PageSize = 100000;
            return ListByEvent(eventAddress, scheduleItemFilterCriteria);
        }

        public OxiteViewModelItems<ScheduleItem> ListByUser(EventAddress eventAddress, ScheduleItemFilterCriteria scheduleItemFilterCriteria)
        {
            return ListByUserImpl(eventAddress, scheduleItemFilterCriteria);
        }

        public OxiteViewModelItems<ScheduleItem> ListByUserPrintable(EventAddress eventAddress, ScheduleItemFilterCriteria scheduleItemFilterCriteria)
        {
            var user = context.User != null ? context.User.Cast<UserAuthenticated>() : null;
            scheduleItemFilterCriteria.PageSize = 200;
            if(user == null)
            {
                return null;
            }
            
            ViewData["Printable"] = true;
            
            return ListByUserImpl(eventAddress, scheduleItemFilterCriteria);
        }

        private OxiteViewModelItems<ScheduleItem> ListByUserImpl(EventAddress eventAddress, ScheduleItemFilterCriteria scheduleItemFilterCriteria)
        {
            Event evnt = eventService.GetEvent(eventAddress);
            if (evnt == null)
            {
                return null;
            }

            scheduleItemFilterCriteria.ForUser = true;

            // note (dcrenna) this still cannot be cached yet so that changes pick up from all views
            var scheduleItems = scheduleItemService.GetScheduleItemsUncachedByUser(eventAddress, scheduleItemFilterCriteria);

            SetUserScheduleStatus();

            return new OxiteViewModelItems<ScheduleItem>(scheduleItems) { Container = evnt };
        }
        
        public OxiteViewModelItems<ScheduleItem> SiteMap(EventAddress eventAddress, ScheduleItemFilterCriteria scheduleItemFilterCriteria)
        {
            Event evnt = eventService.GetEvent(eventAddress);
            if (evnt == null) return null;
            scheduleItemFilterCriteria.PageSize = 2000;
            IPageOfItems<ScheduleItem> scheduleItems = scheduleItemService.GetScheduleItems(eventAddress, scheduleItemFilterCriteria);

            return new OxiteViewModelItems<ScheduleItem>(scheduleItems) { Container = evnt };
        }

        public OxiteViewModelItems<ScheduleItem> ListByEventAndWorkshops(EventAddress eventAddress, TagAddress tagAddress, ScheduleItemFilterCriteria scheduleItemFilterCriteria)
        {
            Event evnt = eventService.GetEvent(eventAddress);
            if (evnt == null) return null;

            Tag tag = tagService.GetTag(tagAddress);
            if (!string.IsNullOrEmpty(tagAddress.TagName) && tag == null) return null;

            ScheduleItemTag scheduleItemTag = scheduleItemService.GetScheduleItemTag(tagService.GetTag(tagAddress));
            if (!string.IsNullOrEmpty(tagAddress.TagName) && scheduleItemTag == null) return null;
            scheduleItemFilterCriteria.PageSize = 100; //show all workshops on workshop page
            IPageOfItems<ScheduleItem> scheduleItems = scheduleItemService.GetScheduleItemsByTag(eventAddress, scheduleItemTag != null ? scheduleItemTag.ID : Guid.Empty, scheduleItemFilterCriteria);

            SetUserScheduleStatus();

            var model = new OxiteViewModelItems<ScheduleItem>(scheduleItems) { Container = evnt };
            model.AddModelItem(scheduleItemTag);

            return model;
        }

        public OxiteViewModelItems<ScheduleItem> ListByEventAndTag(EventAddress eventAddress, TagAddress tagAddress, ScheduleItemFilterCriteria scheduleItemFilterCriteria)
        {
            Event evnt = eventService.GetEvent(eventAddress);
            if (evnt == null) return null;

            Tag tag = tagService.GetTag(tagAddress);
            if (!string.IsNullOrEmpty(tagAddress.TagName) && tag == null) return null;

            ScheduleItemTag scheduleItemTag = scheduleItemService.GetScheduleItemTag(tagService.GetTag(tagAddress));
            if (!string.IsNullOrEmpty(tagAddress.TagName) && scheduleItemTag == null) return null;

            IPageOfItems<ScheduleItem> scheduleItems = scheduleItemService.GetScheduleItemsByTag(eventAddress, scheduleItemTag != null ? scheduleItemTag.ID : Guid.Empty, scheduleItemFilterCriteria);

            SetUserScheduleStatus();

            var model = new OxiteViewModelItems<ScheduleItem>(scheduleItems) { Container = evnt };
            model.AddModelItem(scheduleItemTag);

            return model;
        }

        
        public OxiteViewModelItemItems<ScheduleItemTag, ScheduleItem> ListByEventAndTagAndUser(EventAddress eventAddress, TagAddress tagAddress, ScheduleItemFilterCriteria scheduleItemFilterCriteria)
        {
            Event evnt = eventService.GetEvent(eventAddress);
            if (evnt == null) return null;

            Tag tag = tagService.GetTag(tagAddress);
            if (!string.IsNullOrEmpty(tagAddress.TagName) && tag == null) return null;

            scheduleItemFilterCriteria.ForUser = true;
            
            ScheduleItemTag scheduleItemTag = scheduleItemService.GetScheduleItemTag(tagService.GetTag(tagAddress));
            if (!string.IsNullOrEmpty(tagAddress.TagName) && scheduleItemTag == null) return null;

            IPageOfItems<ScheduleItem> scheduleItems = scheduleItemService.GetScheduleItemsByTag(eventAddress, scheduleItemTag != null ? scheduleItemTag.ID : Guid.Empty, scheduleItemFilterCriteria);

            SetUserScheduleStatus();

            return new OxiteViewModelItemItems<ScheduleItemTag, ScheduleItem>(scheduleItemTag, scheduleItems) { Container = evnt };
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Head)]
        public OxiteViewModelItem<ScheduleItem> Item(ScheduleItemAddress scheduleItemAddress)
        {
            return ItemImpl(scheduleItemAddress);
        }

        private OxiteViewModelItem<ScheduleItem> ItemImpl(ScheduleItemAddress scheduleItemAddress)
        {
            Event evnt = eventService.GetEvent(scheduleItemAddress.ToEventAddress());

            if (evnt == null) return null;

            ScheduleItem scheduleItem = scheduleItemService.GetScheduleItem(scheduleItemAddress);

            if (scheduleItem == null) return null;

            bool includeUnapproved = context.User.IsAuthenticated && context.User.IsInRole("Admin");

            return new OxiteViewModelItem<ScheduleItem>(scheduleItem)
                       {
                           Container = scheduleItem.Event
                       };
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Head)]
        public ActionResult ItemRedirect(ScheduleItemAddress scheduleItemAddress)
        {
            var url = "/MIX10/Sessions/" + scheduleItemAddress.ScheduleItemSlug;
            var redirect = new PermanentRedirectResult(url);

            return redirect;
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Head)]
        [IcsResultFilter]
        public OxiteViewModelItem<ScheduleItem> ItemIcs(ScheduleItemAddress scheduleItemAddress)
        {
            return ItemImpl(scheduleItemAddress);
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Head)]
        [XmlResultFilter]
        public OxiteViewModelItem<ScheduleItem> ItemXml(ScheduleItemAddress scheduleItemAddress)
        {
            return ItemImpl(scheduleItemAddress);
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Head)]
        [JsonResultFilter]
        public OxiteViewModelItem<ScheduleItem> ItemJson(ScheduleItemAddress scheduleItemAddress)
        {
            return ItemImpl(scheduleItemAddress);
        }

        [ActionName("Item"), AcceptVerbs(HttpVerbs.Post)]
        public object AddComment(ScheduleItemAddress scheduleItemAddress, CommentInput commentInput)
        {
            if (context.Site.CommentingDisabled) return null;

            Event evnt = eventService.GetEvent(scheduleItemAddress.ToEventAddress());

            if (evnt == null) return null;

            ScheduleItem scheduleItem = scheduleItemService.GetScheduleItem(scheduleItemAddress);

            if (scheduleItem == null) return null;

            ModelResult<ScheduleItemComment> addCommentResults = commentService.AddComment(scheduleItemAddress, commentInput);

            if (!addCommentResults.IsValid)
            {
                ModelState.AddModelErrors(addCommentResults.ValidationState);

                return Item(scheduleItemAddress);
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

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddToUser(ScheduleItemAddress scheduleItemAddress)
        {
            AddToUserJson(scheduleItemAddress);

            var url = Request.UrlReferrer.AbsoluteUri;
            return Redirect(url);
        }

        [AcceptVerbs(HttpVerbs.Post | HttpVerbs.Get)]
        public JsonResult AddToUserJson(ScheduleItemAddress scheduleItemAddress)
        {
            bool isValid;
            var result = ValidateScheduleItemAddress(scheduleItemAddress, out isValid);

            if (!isValid)
            {
                return result;
            }

            var user = context.User.Cast<UserAuthenticated>();
            var userId = user != null ? user.ID : Guid.Empty;

            scheduleItemService.AddUserToScheduleItem(scheduleItemAddress, userId);
            return result;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RemoveFromUser(ScheduleItemAddress scheduleItemAddress)
        {
            RemoveFromUserJson(scheduleItemAddress);

            var url = Request.UrlReferrer.AbsoluteUri;
            return Redirect(url);
        }

        [AcceptVerbs(HttpVerbs.Post | HttpVerbs.Get)]
        public JsonResult RemoveFromUserJson(ScheduleItemAddress scheduleItemAddress)
        {
            bool isValid;
            var result = ValidateScheduleItemAddress(scheduleItemAddress, out isValid);

            if (!isValid)
            {
                return result;
            }

            var user = context.User.Cast<UserAuthenticated>();
            var userId = user != null ? user.ID : Guid.Empty;

            scheduleItemService.RemoveUserFromScheduleItem(scheduleItemAddress, userId);
            return result;
        }

        private JsonResult ValidateScheduleItemAddress(ScheduleItemAddress scheduleItemAddress, out bool isValid)
        {
            var @event = eventService.GetEvent(scheduleItemAddress.ToEventAddress());
            var success = new JsonResult { Data = new { result = "success"} };
            var failure = new JsonResult { Data = new { result = "error", message = "invalid data"} };

            if (@event == null)
            {
                isValid = false;
                return failure;
            }

            var scheduleItem = scheduleItemService.GetScheduleItem(scheduleItemAddress);

            if (scheduleItem == null)
            {
                isValid = false;
                return failure;
            }

            if (!context.User.IsAuthenticated)
            {
                isValid = false;
                return failure;
            }

            isValid = true;
            return success;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ShareSchedule()
        {
            ShareScheduleJson();

            var url = Request.UrlReferrer.AbsoluteUri;
            return Redirect(url);
        }

        [AcceptVerbs(HttpVerbs.Post | HttpVerbs.Get)]
        public JsonResult ShareScheduleJson()
        {
            var success = new JsonResult { Data = new { result = "success" } };
            var failure = new JsonResult { Data = new { result = "error", message = "invalid data" } };

            if (context.User.IsAuthenticated)
            {
                var user = context.User.Cast<UserAuthenticated>();
                var userID = user != null ? user.ID : Guid.Empty;

                var value = Request.QueryString["makeschedulepublic"];
                var makePublic = value != null ? Convert.ToBoolean(value) : false;

                if(makePublic)
                {
                    userScheduleService.MakeUserSchedulePublic(userID);
                }
                else
                {
                    userScheduleService.MakeUserSchedulePrivate(userID);
                }
                
            }
            else
            {
                return failure;
            }

            
            return success;
        }

        private void SetUserScheduleStatus()
        {
            var user = context.User.Cast<UserAuthenticated>();
            if (user != null)
            {
                ViewData["UserScheduleIsPublic"] = userScheduleService.IsUserSchedulePublic(user.ID);
            }
        }
    }
}
