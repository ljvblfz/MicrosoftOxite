//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using Oxite.Infrastructure;
using Oxite.Models;

namespace Oxite.Modules.Blogs.Models
{
    public class Blog : EntityBase, INamedEntity, ICacheEntity, ISecureEntity
    {
        public Blog(string name)
        {
            Name = name;
        }

        public Blog(Guid siteID, bool commentingDisabled, string description, string displayName, string name)
            : this(name)
        {
            Site = new SiteSmall(siteID);
            CommentingDisabled = commentingDisabled;
            Description = description;
            DisplayName = !string.IsNullOrEmpty(displayName) ? displayName : name;
        }

        public Blog(Guid siteID, bool commentingDisabled, DateTime created, string description, string displayName, Guid id, DateTime modified, string name)
            : base(id)
        {
            Site = new SiteSmall(siteID);
            Name = name;
            DisplayName = !string.IsNullOrEmpty(displayName) ? displayName : name;
            Description = description;
            CommentingDisabled = commentingDisabled;
            Created = created;
            Modified = modified;
        }

        public string Name { get; private set; }
        public string DisplayName { get;  private set; }
        public SiteSmall Site { get; private set; }
        public string Description { get; private set; }
        public bool CommentingDisabled { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime Modified { get; private set; }

        #region ISecureEntity Members

        public bool IsInRole(UserAuthenticated user, string role)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ICacheEntity Members

        IEnumerable<ICacheEntity> ICacheEntity.GetCacheDependencyItems()
        {
            List<ICacheEntity> dependencies = new List<ICacheEntity>();

            dependencies.Add(Site);

            return dependencies;
        }

        #endregion
    }
}
