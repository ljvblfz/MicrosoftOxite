//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Oxite.Modules.Blogs.Models;

namespace Oxite.Plugins.Models
{
    public class PostForProcessing
    {
        private readonly Post original;

        public PostForProcessing(Post post)
        {
            original = post;

            Blog = new BlogReadOnly(post.Blog);
            Body = post.Body;
            Excerpt = post.BodyShort;
            Comments = post.Comments.Select(c => new CommentReadOnly(c, "")).ToArray();
            CommentingDisabled = post.CommentingDisabled;
            Created = post.Created;
            Creator = new UserReadOnly(post.Creator);
            Modified = post.Modified;
            Published = post.Published;
            Slug = post.Slug;
            IsPending = post.State == Oxite.Models.EntityState.PendingApproval;
            Tags = post.Tags.Select(t => new TagReadOnly(t)).ToArray();
            Title = post.Title;
            Trackbacks = post.Trackbacks.Select(tb => new TrackbackReadOnly(tb)).ToArray();
        }

        public BlogReadOnly Blog { get; private set; }
        public string Body { get; set; }
        public IEnumerable<CommentReadOnly> Comments { get; private set; }
        public string Excerpt { get; set; }
        public bool CommentingDisabled { get; private set; }
        public DateTime Created { get; private set; }
        public UserReadOnly Creator { get; private set; }
        public DateTime Modified { get; private set; }
        public DateTime? Published { get; private set; }
        public string Slug { get; set; }
        public bool IsPending { get; private set; }
        public IEnumerable<TagReadOnly> Tags { get; private set; }
        public string Title { get; set; }
        public IEnumerable<TrackbackReadOnly> Trackbacks { get; private set; }

        public Post ToPost()
        {
            return new Post(original.Blog, Body, Excerpt, original.CommentingDisabled, original.Created, original.Creator, original.ID, original.Modified, original.Published, Slug, original.State, original.Tags, Title, original.Comments, original.Trackbacks);
        }
    }
}
