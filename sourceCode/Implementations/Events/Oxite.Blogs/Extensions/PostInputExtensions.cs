//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using Oxite.Modules.Blogs.Models;
using System.Collections.Generic;
using System.Linq;

namespace Oxite.Modules.Blogs.Extensions
{
    public static class PostInputExtensions
    {
        public static PostInput NormalizeTags(this PostInput post, Func<string, string> normalizeTag)
        {
            if (post.Tags == null || post.Tags.Count() == 0) return post;

            List<string> tags = new List<string>(post.Tags.Count());

            post.Tags.ToList().ForEach(t => tags.Add(normalizeTag(t)));

            return new PostInput(post.BlogName, post.Title, post.Body, post.BodyShort, tags, post.Slug, post.Published, post.CommentingDisabled);
        }
    }
}
