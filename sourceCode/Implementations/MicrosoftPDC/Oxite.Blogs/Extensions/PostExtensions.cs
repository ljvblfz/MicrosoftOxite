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
using Oxite.Modules.Blogs.Models;

namespace Oxite.Modules.Blogs.Extensions
{
    public static class PostExtensions
    {
        public static Post Apply(this Post post, PostInput input, EntityState state, UserAuthenticated creator, Func<string, string> normalizeTag)
        {
            List<PostTag> tags = new List<PostTag>(post.Tags.Count());

            foreach (string tagName in input.Tags)
                tags.Add(new PostTag(normalizeTag(tagName), tagName));

            return new Post(post.Blog, input.Body, input.BodyShort, input.CommentingDisabled, post.Created, creator, post.ID, post.Modified, input.Published, input.Slug, state, tags, input.Title, post.Comments, post.Trackbacks, post.Files);
        }

        public static PostAddress ToPostAddress(this Post post)
        {
            return new PostAddress(post.Blog.ToBlogAddress(), post.Slug);
        }

        public static IEnumerable<ICacheEntity> GetDependencies(this Post post)
        {
            List<ICacheEntity> dependencies = new List<ICacheEntity>();

            if (post == null)
                return dependencies;

            dependencies.Add(post);

            dependencies.Add(post.Blog);

            dependencies.AddRange(post.Tags.Cast<ICacheEntity>());

            return dependencies;
        }
    }
}
