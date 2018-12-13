//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Oxite.Modules.Tags.Models;

namespace Oxite.Modules.Tags.Repositories.SqlServer
{
    public class SqlServerTagRepository : ITagRepository
    {
        private readonly OxiteTagsDataContext context;

        public SqlServerTagRepository(OxiteTagsDataContext context)
        {
            this.context = context;
        }

        #region ITagRepository Members

        public IEnumerable<Tag> GetTags()
        {
            return (
                from t in context.oxite_Tags
                join pt in context.oxite_Tags on t.ParentTagID equals pt.TagID
                select new Tag(t.TagID, t.TagName, t.CreatedDate)
                ).ToArray();
        }

        public Tag GetTag(Guid id)
        {
            return (
                from t in context.oxite_Tags
                where t.TagID == id
                select new Tag(t.TagID, t.TagName, t.CreatedDate)
                ).FirstOrDefault();
        }

        public Tag GetTag(string tagName)
        {
            return (
                from t in context.oxite_Tags
                where t.TagName == tagName
                select new Tag(t.TagID, t.TagName, t.CreatedDate)
                ).FirstOrDefault();
        }

        #endregion
    }
}
