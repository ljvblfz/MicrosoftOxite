//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Extensions;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.CMS.Extensions;
using Oxite.Modules.CMS.Models;
using Oxite.Modules.CMS.Services;
using Oxite.Results;
using Oxite.Services;
using Oxite.Validation;
using Oxite.ViewModels;

namespace Oxite.Modules.CMS.Controllers
{
    public class PageController : Controller
    {
        private readonly IPageService pageService;

        public PageController(IPageService pageService)
        {
            this.pageService = pageService;
            ValidateRequest = false;
        }

        public OxiteViewModelItems<Page> List()
        {
            return new OxiteViewModelItems<Page>(pageService.GetPages());
        }

        public OxiteViewModelItems<Page> SiteMap()
        {
            return new OxiteViewModelItems<Page>(pageService.GetPages());
        }

        
        public OxiteViewModelItem<Page> Item(PageAddress pageAddress)
        {
            //TODO: (erikpo) Check permissions to see if the current user is allowed to see this page or not
            Page page = pageService.GetPage(pageAddress);

            return page == null ? null : new OxiteViewModelItem<Page>(page);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Validate(PageInput pageInput)
        {
            ValidationStateDictionary validationState = pageService.ValidatePageInput(pageInput);

            if (validationState.IsValid) return Content("");

            return PartialView("ValidationErrors", new OxiteViewModelPartial<ValidationStateDictionary>(new OxiteViewModel(), validationState));
        }

        [ActionName("ItemAdd"), AcceptVerbs(HttpVerbs.Get)]
        public OxiteViewModelItem<PageInput> Add(PageInput pageInput)
        {
            //TODO: (erikpo) Check permissions

            return new OxiteViewModelItem<PageInput>(pageInput != null ? pageInput : new PageInput(pageInput.TemplateName, pageInput.Title, pageInput.Description, pageInput.Slug, pageInput.Published));
        }

        [ActionName("ItemAdd"), AcceptVerbs(HttpVerbs.Post)]
        public object AddSave(PageInput pageInput)
        {
            //TODO: (erikpo) Check permissions

            ModelResult<Page> results = pageService.AddPage(pageInput);

            if (!results.IsValid)
            {
                ModelState.AddModelErrors(results.ValidationState);

                return Add(pageInput);
            }

            return Redirect(Url.AppPath(Url.Page(results.Item)));
        }

        [ActionName("ItemEdit"), AcceptVerbs(HttpVerbs.Get)]
        public OxiteViewModelItem<PageInput> Edit(PageAddress pageAddress, PageInput pageInput)
        {
            //TODO: (erikpo) Check permissions

            Page page = pageService.GetPage(pageAddress);

            if (page == null) return null;

            var model = new OxiteViewModelItem<PageInput>(new PageInput(page, pageInput));

            model.AddModelItem(pageService.GetPage(pageAddress));

            return model;
        }

        [ActionName("ItemEdit"), AcceptVerbs(HttpVerbs.Post)]
        public object SaveEdit(PageAddress pageAddress, PageInput pageInput)
        {
            //TODO: (erikpo) Check permissions

            ModelResult<Page> results = pageService.EditPage(pageAddress, pageInput);

            if (!results.IsValid)
            {
                ModelState.AddModelErrors(results.ValidationState);

                return Edit(pageAddress, pageInput);
            }

            return Redirect(Url.AppPath(Url.Page(results.Item)));
        }

        [ActionName("ItemEditContent"), AcceptVerbs(HttpVerbs.Post)]
        public object SaveEditContent(PageAddress pageAddress, ContentItemsInput contentItemsInput)
        {
            //TODO: (erikpo) Check permissions

            pageService.EditPageContent(pageAddress, contentItemsInput);

            //todo: (nheskew) get this all set up to edit a single content item and link back down to that content. probably needs a success message too
            return Redirect(Url.Page(pageService.GetPage(pageAddress)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Remove(PageAddress pageAddress, string returnUri)
        {
            //TODO: (erikpo) Check permissions

            pageService.RemovePage(pageAddress);

            return Redirect(returnUri);
        }
    }
}
