//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using Oxite.Modules.Tags.Infrastructure;
using Oxite.Modules.Tags.Models;

namespace Oxite.Modules.Tags.Services
{
    public interface ITagService
    {
        IEnumerable<Tag> GetTags();
        Tag GetTag(string tagName);
        void FillTags(ITaggedEntity entity);
        void FillTag(Tag tag);
    }
}
