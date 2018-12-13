//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Modules.Conferences.Models;
using Oxite.Modules.Conferences.Services;

namespace Oxite.Modules.Conferences.Controllers
{
    public class CommentController : Controller
    {
        private readonly IConferencesCommentService commentService;

        public CommentController(IConferencesCommentService commentService)
        {
            this.commentService = commentService;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Approve(ScheduleItemCommentAddress commentAddress, string returnUri)
        {
            //TODO: (erikpo) Check permissions

            if (commentService.ApproveComment(commentAddress))
            {
                if (!string.IsNullOrEmpty(returnUri)) return new RedirectResult(returnUri);

                return new JsonResult { Data = true };
            }
            else
            {
                return new JsonResult { Data = false };
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Remove(ScheduleItemCommentAddress commentAddress, string returnUri)
        {
            //TODO: (erikpo) Check permissions

            if (commentService.RemoveComment(commentAddress))
            {
                if (!string.IsNullOrEmpty(returnUri)) return new RedirectResult(returnUri);

                return new JsonResult { Data = true };
            }
            else
            {
                return new JsonResult { Data = false };
            }
        }
    }
}