//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Oxite.Models;
using Oxite.Modules.Tags.Models;

namespace Oxite.Modules.Blogs.Models
{
    public class PostInput
    {
        public PostInput(string blogName, string title, string body, string bodyShort, IEnumerable<string> tags, string slug, DateTime? published, bool commentingDisabled)
        {
            BlogName = blogName;
            Title = title;
            Body = body;
            BodyShort = bodyShort;
            Tags = tags;
            Slug = slug;
            Published = published;
            CommentingDisabled = commentingDisabled;
        }

        public PostInput(Blog blog, PostInput postInput)
        {
            if (postInput != null)
            {
                BlogName = postInput.BlogName;
                Title = postInput.Title;
                Body = postInput.Body;
                BodyShort = postInput.BodyShort;
                Tags = postInput.Tags;
                Slug = postInput.Slug;
                Published = postInput.Published;
                CommentingDisabled = postInput.CommentingDisabled;
            }
            else
            {
                BlogName = blog != null ? blog.Name : "";
                Title = "";
                Body = "";
                BodyShort = "";
                Tags = Enumerable.Empty<string>();
                Slug = "";
                Published = null;
                CommentingDisabled = false;
            }
        }

        public PostInput(Post post, PostInput postInput)
        {
            if (postInput != null)
            {
                BlogName = postInput.BlogName;
                Title = postInput.Title;
                Body = postInput.Body;
                BodyShort = postInput.BodyShort;
                Tags = postInput.Tags;
                Slug = postInput.Slug;
                Published = postInput.Published;
                CommentingDisabled = postInput.CommentingDisabled;
            }
            else
            {
                BlogName = post.Blog.Name;
                Title = post.Title;
                Body = post.Body;
                BodyShort = post.BodyShort;
                Tags = post.Tags.Select(t => !string.IsNullOrEmpty(t.DisplayName) ? t.DisplayName : t.Name).ToList();
                Slug = post.Slug;
                Published = post.Published;
                CommentingDisabled = post.CommentingDisabled;
            }
        }

        public string BlogName { get; private set; }
        public string Title { get; private set; }
        public string Body { get; private set; }
        public string BodyShort { get; private set; }
        public IEnumerable<string> Tags { get; private set; }
        public string Slug { get; private set; }
        public DateTime? Published { get; private set; }
        public bool CommentingDisabled { get; private set; }

        public Post ToPost(EntityState state, UserAuthenticated creator, Func<string, string> normalizeTag)
        {
            List<PostTag> tags = new List<PostTag>(Tags.Count());

            foreach (string tagName in Tags)
                tags.Add(new PostTag(normalizeTag(tagName), tagName));

            return new Post(new Blog(BlogName), Body, BodyShort, CommentingDisabled, creator, Published, Slug, state, tags, Title);
        }
    }
}
