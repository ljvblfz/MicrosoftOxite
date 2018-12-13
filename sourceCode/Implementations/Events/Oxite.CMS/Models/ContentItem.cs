//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using Oxite.Models;

namespace Oxite.Modules.CMS.Models
{
    public class ContentItem : EntityBase, INamedEntity
    {
        public ContentItem(Guid id, SiteSmall site, Page page, string name, string displayName, string body, short version, UserAuthenticated creator, DateTime created, DateTime? published)
            : base(id)
        {
            Site = site;
            Page = page;
            Name = name;
            DisplayName = displayName;
            Body = body;
            Version = version;
            Creator = creator;
            Created = created;
            Published = published;
        }

        public ContentItem(Guid siteID, Guid pageID, string name, string displayName, string body, UserAuthenticated creator, DateTime? published)
        {
            Site = new SiteSmall(siteID);
            Page = pageID != Guid.Empty ? new Page(pageID) : null;
            Name = name;
            DisplayName = displayName;
            Body = body;
            Creator = creator;
            Published = published;
        }

        public SiteSmall Site { get; private set; }
        public Page Page { get; private set; }
        public string Body { get; private set; }
        public short Version { get; private set; }
        public UserAuthenticated Creator { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime? Published { get; private set; }

        #region INamedEntity Members

        public string Name { get; private set; }
        public string DisplayName { get; private set; }

        #endregion
    }
}
