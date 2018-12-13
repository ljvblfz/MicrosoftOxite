//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using Oxite.Modules.Blogs.Models;

namespace Oxite.Plugins.Models
{
    public class TrackbackReadOnly
    {
        public TrackbackReadOnly(Trackback trackback)
        {
            Url = trackback.Url;
            Title = trackback.Title;
            Body = trackback.Body;
            BlogName = trackback.BlogName;
            Source = trackback.Source;
            IsTargetInSource = trackback.IsTargetInSource;
            Created = trackback.Created.Value;
            Modified = trackback.Modified.Value;
        }

        public string Url { get; private set; }
        public string Title { get; private set; }
        public string Body { get; private set; }
        public string BlogName { get; private set; }
        public string Source { get; private set; }
        public bool? IsTargetInSource { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime Modified { get; private set; }
    }
}
