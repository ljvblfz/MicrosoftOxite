//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.CMS.Extensions;
using Oxite.Modules.CMS.Models;
using Oxite.Modules.CMS.Repositories;
using Oxite.Plugins.Extensions;
using Oxite.Services;
using Oxite.Validation;

namespace Oxite.Modules.CMS.Services
{
    public class PageService : IPageService
    {
        private readonly IPageRepository repository;
        private readonly IValidationService validator;
        private readonly IPluginEngine pluginEngine;
        private readonly IOxiteCacheModule cache;
        private readonly OxiteContext context;

        public PageService(IPageRepository repository, IValidationService validator, IPluginEngine pluginEngine, IModulesLoaded modules, OxiteContext context)
        {
            this.repository = repository;
            this.validator = validator;
            this.pluginEngine = pluginEngine;
            this.cache = modules.GetModules<IOxiteCacheModule>().Reverse().First();
            this.context = context;
        }

        #region IPageService Members

        public Page GetPage(PageAddress pageAddress)
        {
            return repository.GetPage(context.Site.ID, pageAddress.Slug);
        }

        public IEnumerable<Page> GetPages()
        {
            return repository.GetPages(context.Site.ID).ToArray();
        }

        public ValidationStateDictionary ValidatePageInput(PageInput pageInput)
        {
            ValidationStateDictionary validationState = new ValidationStateDictionary();

            validationState.Add(typeof(PageInput), validator.Validate(pageInput));

            return validationState;
        }

        public ModelResult<Page> AddPage(PageInput pageInput)
        {
            pageInput = pluginEngine.Process<PluginPageInput>("ProcessInputOfPage", new PluginPageInput(pageInput)).ToPageInput();
            pageInput = pluginEngine.Process<PluginPageInput>("ProcessInputOfPageOnAdd", new PluginPageInput(pageInput)).ToPageInput();

            ValidationStateDictionary validationState = ValidatePageInput(pageInput);

            if (!validationState.IsValid) return new ModelResult<Page>(validationState);

            Page page;

            using (TransactionScope transaction = new TransactionScope())
            {
                page = pageInput.ToPage(context.User.Cast<UserAuthenticated>(), context.Site.ID);

                validatePage(page, validationState);

                if (!validationState.IsValid) return new ModelResult<Page>(validationState);

                page = repository.Save(page);

                invalidateCachedPageDependencies(page);

                transaction.Complete();
            }

            //pluginEngine.ExecuteAll("PageSaved", new { context, page = new PageReadOnly(page) });
            //pluginEngine.ExecuteAll("PageAdded", new { context, page = new PageReadOnly(page) });

            return new ModelResult<Page>(page, validationState);
        }

        public ModelResult<Page> EditPage(PageAddress pageAddress, PageInput pageInput)
        {
            pageInput = pluginEngine.Process<PluginPageInput>("ProcessInputOfPage", new PluginPageInput(pageInput)).ToPageInput();
            pageInput = pluginEngine.Process<PluginPageInput>("ProcessInputOfPageOnEdit", new PluginPageInput(pageInput)).ToPageInput();

            ValidationStateDictionary validationState = ValidatePageInput(pageInput);

            if (!validationState.IsValid) return new ModelResult<Page>(validationState);

            Page originalPage;
            Page newPage;
            bool isPublished;

            using (TransactionScope transaction = new TransactionScope())
            {
                originalPage = GetPage(pageAddress);
                isPublished = originalPage.Published.HasValue && originalPage.Published.Value <= DateTime.UtcNow;
                newPage = originalPage.Apply(pageInput, context.Site.ID, context.User.Cast<UserAuthenticated>(), EntityState.Normal);

                validatePage(newPage, originalPage, validationState);

                if (!validationState.IsValid) return new ModelResult<Page>(validationState);

                newPage = repository.Save(newPage);

                invalidateCachedPageForEdit(newPage, originalPage);

                transaction.Complete();
            }

            //pluginEngine.ExecuteAll("PageSaved", new { context, page = new PageReadOnly(newPage) });
            //pluginEngine.ExecuteAll("PageEdited", new { context, page = new PageReadOnly(newPage), pageOriginal = new PageReadOnly(originalPage) });

            bool isNowPublished = newPage.Published.HasValue && newPage.Published.Value <= DateTime.UtcNow;
            //if (!isPublished && isNowPublished)
            //    pluginEngine.ExecuteAll("PagePublished", new { context, page = new PageReadOnly(newPage) });
            //else if (isPublished && !isNowPublished)
            //    pluginEngine.ExecuteAll("PageUnpublished", new { context, page = new PageReadOnly(newPage) });

            return new ModelResult<Page>(newPage, validationState);
        }

        public void RemovePage(PageAddress pageAddress)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                Page page = GetPage(pageAddress);

                if (page != null)
                {
                    if (repository.Remove(page.ID))
                    {
                        invalidateCachedPageForRemove(page);

                        transaction.Complete();

                        //pluginEngine.ExecuteAll("PageRemoved", new { context, page = new PageReadOnly(page) });

                        return;
                    }
                }
            }
        }

        public IEnumerable<ContentItem> GetContentItems(Page page)
        {
            return repository.GetContentItems(context.Site.ID, page.ID);
        }

        public void EditPageContent(PageAddress pageAddress, ContentItemsInput contentItemsInput)
        {
            //TODO: (erikpo) Validate input

            Page page = GetPage(pageAddress);

            using (TransactionScope transaction = new TransactionScope())
            {
                //TODO: (erikpo) Follow other editing patterns in Oxite and add plugin and module events

                foreach (ContentItemInput input in contentItemsInput.ContentItems)
                    repository.Save(input.ToContentItem(context.User.Cast<UserAuthenticated>(), context.Site.ID, page.ID));

                transaction.Complete();
            }
        }

        #endregion

        #region Private Methods

        private void invalidateCachedPageDependencies(Page page)
        {
            //Page parent = page.Parent;

            //while (parent != null)
            //{
            //    cache.InvalidateItem(parent);

            //    parent = parent.Parent;
            //}

            //cache.InvalidateItem(page.Site);
        }

        private void invalidateCachedPageForEdit(Page newPage, Page originalPage)
        {
            //if ((originalPage.Parent != null ? originalPage.Parent.ID : Guid.Empty) != (newPage.Parent != null ? newPage.Parent.ID : Guid.Empty))
            //{
            //    if (originalPage.Parent != null)
            //        invalidateCachedPageDependencies(originalPage.Parent);
            //    if (newPage.Parent != null)
            //        invalidateCachedPageDependencies(newPage.Parent);
            //}

            cache.InvalidateItem(newPage);
        }

        private void invalidateCachedPageForRemove(Page page)
        {
            invalidateCachedPageDependencies(page);

            cache.InvalidateItem(page);
        }

        private Page processDisplayOfPage(Func<Page> getPage)
        {
            //Page page = getPage();
            //PageForProcessing pageProxy = new PageForProcessing(page);

            //pluginEngine.ExecuteAll("ProcessDisplayOfPage", new { context, page = pageProxy });

            return null; //pageProxy.ToPage();
        }

        private void validatePage(Page newPage, ValidationStateDictionary validationState)
        {
            validatePage(newPage, newPage, validationState);
        }

        private void validatePage(Page newPage, Page originalPage, ValidationStateDictionary validationState)
        {
            ValidationState state = new ValidationState();
            Page foundPage;

            validationState.Add(typeof(Page), state);

            foundPage = repository.GetPage(context.Site.ID, newPage.Slug);

            if (foundPage != null && newPage.Slug != originalPage.Slug)
                state.Errors.Add("Page.SlugNotUnique", newPage.Slug, "A page already exists with the same url");
        }

        #endregion
    }
}
