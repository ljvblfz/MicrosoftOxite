//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Oxite.Modules.Tags.Models;

namespace Oxite.Modules.Tags.Repositories
{
    public interface ITagRepository
    {
        IQueryable<Tag> GetTags();
        Tag GetTag(Guid id);
        Tag GetTag(string tagName);
        IEnumerable<Tag> GetTags(IEnumerable<Guid> ids);
    }
}
