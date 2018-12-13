//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.CMS.Models;

namespace Oxite.Modules.CMS.Repositories.SqlServer
{
    public class SqlServerContentItemRepository : IContentItemRepository
    {
        private readonly OxiteCMSDataContext context;
        private readonly Guid siteID;

        public SqlServerContentItemRepository(OxiteCMSDataContext context, OxiteContext oxiteContext)
        {
            this.context = context;
            siteID = oxiteContext.Site.ID;
        }

        #region IContentItemRepository Members

        public ContentItem GetContentItem(string name)
        {
            return GetContentItem(context, siteID, name);
        }

        public IEnumerable<ContentItem> GetContentItems()
        {
            return (
                from ci in context.oxite_CMS_ContentItems
                group ci.Version by ci.ContentItemName into results
                join ci in context.oxite_CMS_ContentItems on new { ContentItemName = results.Key, Version = results.Max() } equals new { ContentItemName = ci.ContentItemName, Version = ci.Version }
                where ci.SiteID == siteID && ci.PageID == null
                select ProjectContentItem(context, ci)
                ).ToArray();
        }

        public ContentItem Save(ContentItem contentItem)
        {
            return SaveContentItem(context, contentItem);
        }

        #endregion

        #region Static Methods

        internal static ContentItem SaveContentItem(OxiteCMSDataContext context, ContentItem contentItem)
        {
            oxite_CMS_ContentItem item =
                contentItem.Page != null
                ? context.oxite_CMS_ContentItems.Where(ci => ci.SiteID == contentItem.Site.ID && ci.PageID == contentItem.Page.ID && string.Compare(ci.ContentItemName, contentItem.Name, true) == 0).OrderByDescending(ci => ci.Version).FirstOrDefault()
                : context.oxite_CMS_ContentItems.Where(ci => ci.SiteID == contentItem.Site.ID && ci.PageID == null && string.Compare(ci.ContentItemName, contentItem.Name, true) == 0).OrderByDescending(ci => ci.Version).FirstOrDefault();
            short version;

            if (item != null)
            {
                if (item.Body == contentItem.Body) return null;

                version = item.Version;

                version++;
            }
            else
                version = 1;

            item = new oxite_CMS_ContentItem
                {
                    SiteID = contentItem.Site.ID,
                    PageID = contentItem.Page != null ? contentItem.Page.ID : (Guid?)null,
                    ContentItemID = contentItem.ID != Guid.Empty ? contentItem.ID : Guid.NewGuid(),
                    ContentItemName = contentItem.Name,
                    ContentItemDisplayName = contentItem.DisplayName,
                    Body = contentItem.Body,
                    CreatedDate = DateTime.UtcNow,
                    CreatorUserID = contentItem.Creator.ID,
                    PublishedDate = contentItem.Published,
                    Version = version
                };

            context.oxite_CMS_ContentItems.InsertOnSubmit(item);

            context.SubmitChanges();

            return GetContentItem(context, item.SiteID, item.ContentItemName);
        }

        public static ContentItem GetContentItem(OxiteCMSDataContext context, Guid siteID, string name)
        {
            return (
                from ci in context.oxite_CMS_ContentItems
                where ci.SiteID == siteID && ci.PageID == null && string.Compare(ci.ContentItemName, name, true) == 0
                orderby ci.Version descending
                select ProjectContentItem(context, ci)
                ).FirstOrDefault();
        }

        public static IEnumerable<ContentItem> ProjectContentItems(OxiteCMSDataContext context, IQueryable<oxite_CMS_ContentItem> ci)
        {
            return ci.Select(contentItem => ProjectContentItem(context, contentItem)).ToList();
        }

        public static ContentItem ProjectContentItem(OxiteCMSDataContext context, oxite_CMS_ContentItem ci)
        {
            return
                new ContentItem(
                    ci.ContentItemID,
                    new SiteSmall(ci.SiteID),
                    projectPage(context, ci.PageID),
                    ci.ContentItemName,
                    ci.ContentItemDisplayName,
                    ci.Body,
                    ci.Version,
                    new User(ci.oxite_User.UserID, ci.oxite_User.Username, ci.oxite_User.DisplayName),
                    ci.CreatedDate,
                    ci.PublishedDate
                    );
        }

        private static Page projectPage(OxiteCMSDataContext context, Guid? pageID)
        {
            if (pageID == null) return null;

            return context.oxite_CMS_Pages.Where(p => p.PageID == pageID.Value).Select(p => new Page(p.PageID, new SiteSmall(p.SiteID), p.TemplateName, p.Title, p.Description, p.Slug, p.PublishedDate)).FirstOrDefault();
        }

        #endregion
    }
}
