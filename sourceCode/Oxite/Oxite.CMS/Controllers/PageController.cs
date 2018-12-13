//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Extensions;
using Oxite.Models;
using Oxite.Modules.CMS.Extensions;
using Oxite.Modules.CMS.Models;
using Oxite.Modules.CMS.Services;
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

        public OxiteViewModelItem<Page> Item(Page page)
        {
            if (page == null) return null;

            //TODO: (erikpo) Check permissions to see if the current user is allowed to see this page or not

            return new OxiteViewModelItem<Page>(page);
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

            return new OxiteViewModelItem<PageInput>(new PageInput(
                pageInput != null ? pageInput.TemplateName : "",
                pageInput != null ? pageInput.Title : "",
                pageInput != null ? pageInput.Description : "",
                pageInput != null ? pageInput.Slug : "",
                pageInput != null ? pageInput.Published : null
                ));
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
        public OxiteViewModelItem<PageInput> Edit(Page page, PageInput pageInput)
        {
            if (page == null) return null;

            //TODO: (erikpo) Check permissions

            var model = new OxiteViewModelItem<PageInput>(new PageInput(page, pageInput));

            model.AddModelItem(page);

            return model;
        }

        [ActionName("ItemEdit"), AcceptVerbs(HttpVerbs.Post)]
        public object SaveEdit(Page page, PageInput pageInput)
        {
            if (page == null) return null;

            //TODO: (erikpo) Check permissions

            ModelResult<Page> results = pageService.EditPage(page, pageInput);

            if (!results.IsValid)
            {
                ModelState.AddModelErrors(results.ValidationState);

                return Edit(page, pageInput);
            }

            return Redirect(Url.AppPath(Url.Page(results.Item)));
        }

        [ActionName("ItemAddContent"), AcceptVerbs(HttpVerbs.Post)]
        public object SaveAddContent(Page page, ContentItemInput contentItemInput)
        {
            if (page == null) return null;

            //TODO: (erikpo) Check permissions

            pageService.AddPageContent(page, contentItemInput);

            //todo: (nheskew) get this all set up to edit a single content item and link back down to that content. probably needs a success message too
            return Redirect(Url.Page(page));
        }

        [ActionName("ItemEditContent"), AcceptVerbs(HttpVerbs.Post)]
        public object SaveEditContent(Page page, ContentItemsInput contentItemsInput)
        {
            if (page == null) return null;

            //TODO: (erikpo) Check permissions

            pageService.EditPageContent(page, contentItemsInput);

            //todo: (nheskew) get this all set up to edit a single content item and link back down to that content. probably needs a success message too
            return Redirect(Url.Page(page));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Remove(Page page, string returnUri)
        {
            if (page == null) return null;

            //TODO: (erikpo) Check permissions

            pageService.RemovePage(page);

            return Redirect(returnUri);
        }
    }
}
