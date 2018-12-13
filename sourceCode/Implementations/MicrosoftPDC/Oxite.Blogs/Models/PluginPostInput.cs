//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Linq;

namespace Oxite.Modules.Blogs.Models
{
    public class PluginPostInput
    {
        private readonly PostInput originalInput;

        public PluginPostInput(PostInput postInput)
        {
            originalInput = postInput;

            Blog = postInput.BlogName;
            Title = postInput.Title;
            Body = postInput.Body;
            Excerpt = postInput.BodyShort;
            Tags = string.Join(", ", postInput.Tags.ToArray());
            Slug = postInput.Slug;
            Published = postInput.Published.HasValue && postInput.Published.Value <= DateTime.Now;
            if (Published)
                PublishedDate = postInput.Published.Value;
            CommentingDisabled = postInput.CommentingDisabled;
        }

        public string Blog { get; private set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Excerpt { get; set; }
        public string Tags { get; set; }
        public string Slug { get; private set; }
        public bool Published { get; private set; }
        public DateTime PublishedDate { get; private set; }
        public bool CommentingDisabled { get; private set; }

        public PostInput ToPostInput()
        {
            return new PostInput(Blog, Title, Body, Excerpt, Tags.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()), Slug, originalInput.Published, originalInput.CommentingDisabled);
        }
    }
}
