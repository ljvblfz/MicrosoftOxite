//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Linq;
using System.Web.Caching;
using Oxite.Models;
using Oxite.Modules.CMS.Models;

namespace Oxite.Modules.CMS.Repositories.SqlServer
{
    public class SqlServerPageRepository : IPageRepository
    {
        private readonly OxiteCMSDataContext context;

        public SqlServerPageRepository(OxiteCMSDataContext context)
        {
            this.context = context;
        }
        static SqlServerPageRepository()
        {
            _cache = System.Web.HttpContext.Current.Cache;   
        }

        private static Cache _cache;

        #region IPageRepository Members

        public Page GetPage(Guid siteID, string slug)
        {

            Page page = null;
            string cacheKey = String.Format("GetPagebySlug {0} {1}", siteID, slug);

            if (slug == null )
            {
                return null;
            }

            try
            {
            if (_cache != null)
            {
                page = _cache[cacheKey] as Page;
            }

            if (page != null)
                return page;

            System.Diagnostics.Debug.WriteLine("GetPage: " + slug);

                IQueryable<oxite_CMS_Page> query =
                    from p in context.oxite_CMS_Pages
                    where p.SiteID == siteID && string.Compare(p.Slug, slug, true) == 0
                    select p;

                page =  projectPages(query).FirstOrDefault();

                if (_cache != null)
                {
                    _cache.Add(cacheKey, page, null, DateTime.Now.AddHours(2),
                               Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }


                return page;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IQueryable<Page> GetPages(Guid siteID)
        {
            IQueryable<oxite_CMS_Page> query =
                from p in context.oxite_CMS_Pages
                where p.SiteID == siteID
                select p;

            return projectPages(query);
        }

        public Page Save(Page page)
        {
            oxite_CMS_Page pageToSave = null;

            if (page.ID != Guid.Empty)
                pageToSave = context.oxite_CMS_Pages.FirstOrDefault(p => p.SiteID == page.Site.ID && p.PageID == page.ID);

            if (pageToSave == null)
            {
                pageToSave = new oxite_CMS_Page();

                pageToSave.SiteID = page.Site.ID;
                pageToSave.PageID = page.ID != Guid.Empty ? page.ID : Guid.NewGuid();

                context.oxite_CMS_Pages.InsertOnSubmit(pageToSave);
            }

            pageToSave.TemplateName = page.TemplateName;
            pageToSave.Title = page.Title;
            pageToSave.Description = page.Description;
            pageToSave.Slug = page.Slug;
            pageToSave.PublishedDate = page.Published;

            context.SubmitChanges();

            return GetPage(pageToSave.SiteID, pageToSave.Slug);
        }

        public bool Remove(Guid pageID)
        {
            return false;

            //oxite_Page foundPage = context.oxite_Pages.FirstOrDefault(p => p.PageID == pageID);
            //bool removedPage = false;

            //if (foundPage != null)
            //{
            //    foundPage.State = (byte)EntityState.Removed;

            //    context.SubmitChanges();

            //    removedPage = true;
            //}

            //return removedPage;
        }

        public ContentItem GetContentItem(Guid siteID, Guid pageID, string name)
        {
            return GetContentItem(context, siteID, pageID, name);
        }

        public IQueryable<ContentItem> GetContentItems(Guid siteID, Guid pageID)
        {
            return
                from ci in context.oxite_CMS_ContentItems
                group ci.Version by new { ContentItemName = ci.ContentItemName, PageIDHasValue = ci.PageID.HasValue, PageIDValue = ci.PageID.Value } into results
                join ci in context.oxite_CMS_ContentItems on new { ContentItemName = results.Key.ContentItemName, PageIDHasValue = results.Key.PageIDHasValue, PageIDValue = results.Key.PageIDValue, Version = results.Max() } equals new { ContentItemName = ci.ContentItemName, PageIDHasValue = ci.PageID.HasValue, PageIDValue = ci.PageID.Value, Version = ci.Version }
                where ci.SiteID == siteID && ci.PageID == pageID
                select SqlServerContentItemRepository.ProjectContentItem(context, ci);
        }

        public ContentItem Save(ContentItem contentItem)
        {
            return SqlServerContentItemRepository.SaveContentItem(context, contentItem);
        }

        #endregion

        #region Static Methods

        public static ContentItem GetContentItem(OxiteCMSDataContext context, Guid siteID, Guid pageID, string name)
        {
            return (
                from ci in context.oxite_CMS_ContentItems
                where ci.SiteID == siteID && ci.PageID == pageID && string.Compare(ci.ContentItemName, name, true) == 0
                select SqlServerContentItemRepository.ProjectContentItem(context, ci)
                ).FirstOrDefault();
        }

        #endregion

        #region Private Methods

        private IQueryable<Page> projectPages(IQueryable<oxite_CMS_Page> pages)
        {
            return
                from p in pages
                let ci = GetContentItems(p.SiteID, p.PageID)
                select new Page(p.PageID, new SiteSmall(p.SiteID), p.TemplateName, p.Title, p.Description, p.Slug, p.PublishedDate);
        }

        #endregion
    }
}
