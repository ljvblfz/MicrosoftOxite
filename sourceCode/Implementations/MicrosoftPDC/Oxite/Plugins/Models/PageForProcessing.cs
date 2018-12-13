//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;

namespace Oxite.Plugins.Models
{
    //public class PageForProcessing
    //{
    //    private readonly Page original;

    //    public PageForProcessing(Oxite.Models.Page page)
    //    {
    //        original = page;

    //        if (page != null)
    //        {
    //            if (page.Parent != null)
    //                Parent = new PageReadOnly(page.Parent);
    //            Creator = new UserReadOnly(page.Creator);
    //            Title = page.Title;
    //            Body = page.Body;
    //            IsPending = page.State == Oxite.Models.EntityState.PendingApproval;
    //            Slug = page.Slug;
    //            Created = page.Created;
    //            Modified = page.Modified;
    //            Published = page.Published;
    //            HasChildren = page.HasChildren;
    //        }
    //    }

    //    public PageReadOnly Parent { get; private set; }
    //    public UserReadOnly Creator { get; private set; }
    //    public string Title { get; set; }
    //    public string Body { get; set; }
    //    public bool IsPending { get; private set; }
    //    public string Slug { get; set; }
    //    public DateTime Created { get; private set; }
    //    public DateTime Modified { get; private set; }
    //    public DateTime? Published { get; private set; }
    //    public bool HasChildren { get; private set; }

    //    public Oxite.Models.Page ToPage()
    //    {
    //        return original != null
    //            ? new Oxite.Models.Page(original.Site.ID, original.ID, original.Parent, original.HasChildren, original.Creator, Title, Body, Slug, original.State, original.Created, original.Modified, original.Published)
    //            : null;
    //    }
    //}
}
