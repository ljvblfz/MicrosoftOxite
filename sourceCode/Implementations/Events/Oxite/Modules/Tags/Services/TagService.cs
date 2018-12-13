//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
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
            return repository.GetTags().ToArray();
        }

        public Tag GetTag(TagAddress tagAddress)
        {
            return repository.GetTag(tagAddress.TagName);
        }

        public void FillTags(ITaggedEntity entity)
        {
            if (entity == null)
                return;

            foreach (Tag tag in entity.GetTags())
                FillTag(tag);
        }

        public void FillTag(Tag tag)
        {
            if (tag != null && tag.ID != Guid.Empty)
            {
                Tag foundTag = repository.GetTag(tag.ID);

                if (foundTag != null)
                    tag.Fill(foundTag.Name, foundTag.Created);
            }
        }

        public void FillTags(IEnumerable<ITaggedEntity> entities)
        {
            List<Tag> allTags = entities.SelectMany(e => e.GetTags()).Distinct().ToList();

            IDictionary<Guid, Tag> filledTags = repository.GetTags(allTags.Select(t => t.ID)).ToDictionary(t => t.ID);

            allTags.ForEach(t => t.Fill(filledTags[t.ID].Name, filledTags[t.ID].Created));
        }

        #endregion

    }
}
