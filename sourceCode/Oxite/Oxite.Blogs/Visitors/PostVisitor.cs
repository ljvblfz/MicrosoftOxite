//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Infrastructure;
using Oxite.Modules.Blogs.Extensions;
using Oxite.Modules.Blogs.Models;
using Oxite.Modules.Tags.Models;

namespace Oxite.Modules.Blogs.Visitors
{
    public class PostVisitor : Visitor
    {
        private readonly UrlHelper urlHelper;

        public PostVisitor(UrlHelper urlHelper)
        {
            this.urlHelper = urlHelper;
        }

        public string Visit(BlogHomePageContainer container)
        {
            return urlHelper.Posts();
        }

        public string Visit(Post post)
        {
            return urlHelper.Post(post);
        }

        //public string Visit(Page page)
        //{
        //    throw new System.NotImplementedException();
        //}

        public string Visit(Blog blog)
        {
            return urlHelper.Posts(blog);
        }

        public string Visit(Blog blog, string dataFormat)
        {
            return urlHelper.Posts(blog, dataFormat);
        }

        public string Visit(Tag tag)
        {
            return urlHelper.Posts(tag);
        }

        public string Visit(Tag tag, string dataFormat)
        {
            return urlHelper.Posts(tag, dataFormat);
        }
    }
}
