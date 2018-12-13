// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Oxite.Infrastructure;
using Oxite.Modules.CMS.Models;
using Oxite.Modules.CMS.Services;
using Oxite.Services;
using Oxite.ViewModels;

namespace Oxite.Modules.CMS.Controllers
{
    public class ContentItemController : Controller
    {
        private readonly IContentItemService contentItemService;

        public ContentItemController(IContentItemService contentItemService)
        {
            this.contentItemService = contentItemService;
            ValidateRequest = false;
        }

        [ActionName("GlobalContentEdit"), AcceptVerbs(HttpVerbs.Get)]
        public OxiteViewModelItems<ContentItemInput> GlobalEdit(IEnumerable<ContentItemInput> contentInput)
        {
            //TODO: (erikpo) Check permissions

            return new OxiteViewModelItems<ContentItemInput>(contentItemService.GetContentItems().Select(ci => new ContentItemInput(ci.Name, ci.DisplayName, ci.Body, ci.Published)));
        }

        [ActionName("GlobalContentEdit"), AcceptVerbs(HttpVerbs.Post)]
        public object GlobalSaveEdit(ContentItemsInput contentItemsInput)
        {
            //TODO: (erikpo) Check permissions

            /*ModelResult<IEnumerable<ContentItem>> results = */contentItemService.EditContentItems(contentItemsInput.ContentItems);

            //if (!results.IsValid)
            //{
            //    ModelState.AddModelErrors(results.ValidationState);

            //    return GlobalEdit(contentInput, currentUser);
            //}

            return GlobalEdit(contentItemsInput.ContentItems); //Redirect(Url.AppPath(Url.Page(results.Item)));
        }
    }
}