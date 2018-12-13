//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using Oxite.Modules.Tags.Infrastructure;
using Oxite.Modules.Tags.Models;
using Oxite.Modules.Tags.Repositories;
using Oxite.Infrastructure;

namespace Oxite.Modules.Tags.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository repository;
        private readonly IRegularExpressions expressions;

        public TagService(ITagRepository repository, IRegularExpressions expressions)
        {
            this.repository = repository;
            this.expressions = expressions;
        }

        #region ITagService Members

        public IEnumerable<Tag> GetTags()
        {
            return repository.GetTags();
        }

        public Tag GetTag(string tagName)
        {
            return repository.GetTag(tagName);
        }

        public void FillTags(ITaggedEntity entity)
        {
            foreach (Tag tag in entity.GetTags())
                FillTag(tag);
        }

        public void FillTag(Tag tag)
        {
            if (tag.ID != Guid.Empty)
            {
                Tag foundTag = repository.GetTag(tag.ID);

                if (foundTag != null)
                    tag.Fill(foundTag.Name, foundTag.Created);
            }
        }

        #endregion
    }
}
